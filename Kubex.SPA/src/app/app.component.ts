import { Component, OnInit } from '@angular/core';
import { AccountService, AlertService } from './_services';
import { User, Post } from './_models';
import { PostService } from './_services/post.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Kubex';
  user: User;
  private reloaded = false;

  constructor(private accountService: AccountService) {  }

  ngOnInit() {
    this.accountService.user.subscribe(user => {
      this.user = user;
    });
  }
}
