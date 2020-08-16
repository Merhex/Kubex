import { Component } from '@angular/core';
import { AccountService } from './_services';
import { User } from './_models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Kubex';
  user: User;
  photoUrl: string;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
}

  logout() {
      this.accountService.logout();
  }
}
