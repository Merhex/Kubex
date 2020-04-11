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
  selectedUser = null;

  users: Observable<any> = this.http.get('http://localhost:3000/users/');
  // users: Observable<any> = this.http.get('api/users');

  displayUser(userId) {
    return this.http.get('http://localhost:3000/users/' + userId).subscribe(response => {
      // this.outputUser.emit(response);
      this.selectedUser = response;
    });
  }

  constructor(private http: HttpClient) {}

  ngOnInit() {}

}
