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
      this.accountService.user.subscribe(x => {
        console.log(x.userName);
        this.user = JSON.parse(localStorage.getItem('user'));
      });
      this.photoUrl = this.user.photoUrl;
      console.log('user bij aanmelden: ' + this.user.userName);
      console.log('photo url bij inloggen: ' + this.photoUrl);
  }

  logout() {
      this.accountService.logout();
  }
}
