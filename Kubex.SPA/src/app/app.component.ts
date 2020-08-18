import { Component, OnInit } from '@angular/core';
import { AccountService, AlertService } from './_services';
import { User, Post } from './_models';
import { PostService } from './_services/post.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Kubex';
  user: User;
  photoUrl: string;
  posts: Post[];
  private reloaded = false;

  constructor(
    private accountService: AccountService,
    private postService: PostService,
    private alertService: AlertService) {  }

  ngOnInit() {
    this.accountService.user.subscribe(x => {
      this.user = JSON.parse(localStorage.getItem('user'));
    });
    if (this.user) {
      this.photoUrl = this.user.photoUrl;
      this.postService.getPostsForUser(this.user.userName).subscribe(
        (data: Post[]) => {
          this.posts = data;
          console.log(data);
        },
        (error) => {
          this.alertService.error(error);
        }
      );
    }
  }

  logout() {
    this.photoUrl = null;
    this.user = null;
    this.accountService.logout();
  }
}
