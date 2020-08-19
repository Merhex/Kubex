import { Component, OnInit } from '@angular/core';
import { AccountService, AlertService } from '../_services';
import { User, Post } from '../_models';
import { PostService } from '../_services/post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  user: User;
  isAdmin = false;

  constructor(private accountService: AccountService) {
      this.accountService.user.subscribe(user => {
      this.user = user;
      });
  }

  ngOnInit() {
    this.accountService.user.subscribe(user => {
      this.user = user;
    });

    if (this.user.roles.includes('Administrator')) {
      this.isAdmin = true;
    }
  }

}
