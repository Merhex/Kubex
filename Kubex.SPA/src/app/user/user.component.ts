import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  // @Input() inputUser: UserlistComponent;
  // @Output() outputUser = new EventEmitter<any>();
  selectedUser: User = null;

  // users: Observable<any> = this.http.get('http://localhost:3000/users/');
  // users: Observable<any> = this.http.get('api/users');
  users: User[];

  displayUser(userId) {
    this.userService.getUser(userId).subscribe(user => {
      this.selectedUser = user;
    });
  }

  constructor(private userService: UserService) {
    userService.getAllUsers().subscribe(users => {
      this.users = users;
    });
  }

  ngOnInit() {}

}
