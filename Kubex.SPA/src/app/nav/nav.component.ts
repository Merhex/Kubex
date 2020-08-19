import { Component, OnInit } from '@angular/core';
import { User, Post } from '../_models';
import { AccountService, AlertService } from '../_services';
import { PostService } from '../_services/post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  user: User;
  photoUrl: string;
  posts: Post[];

  constructor(private accountService: AccountService,
              private postService: PostService,
              private alertService: AlertService,
              private router: Router) {
    this.accountService.user.subscribe(user => {
      this.user = user;
    });
  }

  ngOnInit() {
    this.accountService.user.subscribe(user => {
      this.user = user;
      this.photoUrl = this.user.photoUrl;
    });

    this.getPosts();
  }

  getPosts() {
    this.postService.getPostsForUser(this.user.userName).subscribe(
      (data: Post[]) => {
        this.posts = data;
      },
      (error) => {
        this.alertService.error(error);
      }
    );
  }

  fetchPostNames() {
    this.getPosts();
  }

  logout() {
    this.photoUrl = null;
    this.user = null;
    this.accountService.logout();
  }

  navigateToPostDar(postId: number) {
    console.log('clicked post link');
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['dar/', { postId }])
    );
  }
}
