import { PostCreate } from './../../_models/postCreate';
import { Company, Post, User } from 'src/app/_models';
import { PostsAddDialogComponent } from './../postsAddDialog/postsAddDialog.component';
import { Component, Input, OnInit, Output, EventEmitter, ChangeDetectorRef, OnChanges } from '@angular/core';
import { ControlContainer } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { PostService } from 'src/app/_services/post.service';
import { first } from 'rxjs/operators';
import { AlertService, AccountService } from 'src/app/_services';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  @Input() company: Company;
  @Output() companyChange = new EventEmitter<Post>();
  posts: Array<Post>;

  constructor(public controlContainer: ControlContainer,
              public changeDetection: ChangeDetectorRef,
              private postService: PostService,
              private alertService: AlertService,
              private accountService: AccountService,
              public dialog: MatDialog
              ) {}

  ngOnInit() {
    this.posts = this.company.posts;
  }

  openDialog(post?: Post) {
    let newPost: Post;

    if (post) {
      newPost = post;
    } else {
      newPost = new PostCreate();
      newPost.address = this.company.address;
    }

    newPost.companyId = this.company.id;
    newPost.company = this.company;

    const dialogRef = this.dialog.open(PostsAddDialogComponent, {
      width: '75%',
      data: newPost
    });

    dialogRef.afterClosed().subscribe(result => {
      if (post) {
        this.postService.update(result)
        .pipe(first())
        .subscribe(
          (data) => {
            let userToUpdate: User;

            this.accountService.user.subscribe(user => {
              userToUpdate = user;
            });

            userToUpdate.postIds.push(data.id);

            this.accountService.updateUser(userToUpdate);
            console.log(userToUpdate);

            // verwijder de bestaande post van de company
            const index = this.company.posts.indexOf(post);
            this.company.posts.splice(index, 1);

            // voeg de geupdate post terug toe
            this.company.posts.push(result);
            this.companyChange.emit(result);

            this.alertService.success('Post successfully updated', { keepAfterRouteChange: true });
          },
          (err) => {
            this.alertService.error(err);
          });
      } else {
        this.postService.create(result)
          .pipe(first())
          .subscribe(
            (data) => {
              let userToUpdate: User;

              this.accountService.user.subscribe(user => {
                userToUpdate = user;
              });

              userToUpdate.postIds.push(data.id);

              this.accountService.updateUser(userToUpdate);
              console.log(userToUpdate);

              this.company.posts.push(result);
              this.companyChange.emit(result);

              this.alertService.success('Post successfully registered', { keepAfterRouteChange: true });
            },
            (err) => {
                this.alertService.error(err);
            });
      }
    });
  }

  deletePost(post: Post) {
    if (post.id === null) {
      const index = this.company.posts.indexOf(post);
      this.company.posts.splice(index, 1);
    } else {
      this.postService.delete(post.id)
      .pipe(first())
        .subscribe(() => {
            this.company.posts = this.company.posts.filter(x => x.id !== post.id);
        },
        error => {
            this.alertService.error(error);
        });
    }
  }
}
