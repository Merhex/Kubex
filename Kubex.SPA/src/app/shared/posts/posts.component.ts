import { PostCreate } from './../../_models/postCreate';
import { Company, Post } from 'src/app/_models';
import { PostsAddDialogComponent } from './../postsAddDialog/postsAddDialog.component';
import { Component, Input, OnInit, Output, EventEmitter, ChangeDetectorRef, OnChanges } from '@angular/core';
import { ControlContainer } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { PostService } from 'src/app/_services/post.service';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/_services';

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
              public dialog: MatDialog
              ) {}

  ngOnInit() {
    this.posts = this.company.posts;
  }

  openDialog() {
    const newPost = new PostCreate();
    newPost.address = this.company.address;
    console.log('address: ' + newPost.address.street);
    newPost.companyId = this.company.id;
    newPost.company = this.company;


    const dialogRef = this.dialog.open(PostsAddDialogComponent, {
      width: '75%',
      data: newPost
    });

    dialogRef.afterClosed().subscribe(result => {
      this.postService.create(result)
                      .pipe(first())
                      .subscribe(
                        (data) => {
                          this.alertService.success('Company successfully registered',  { keepAfterRouteChange: true });
                          this.company.posts.push(result);
                          this.companyChange.emit(result);
                        },
                        (err) => {
                            this.alertService.error(err);
                        });
    });
  }

  deletePost(post: Post) {
    if (post.id == null) {
      this.postService.delete(post.id)
      .pipe(first())
        .subscribe(() => {
            this.company.posts = this.company.posts.filter(x => x.id !== post.id);
        },
        error => {
            this.alertService.error(error);
        });
    } else {
      const index = this.company.posts.indexOf(post);
      this.company.posts.splice(index, 1);
    }
  }
}
