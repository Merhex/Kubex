import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import {SelectItem} from 'primeng/api';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent implements OnInit {

  // users: Observable<any> = this.http.get('http://localhost:3000/api/users');
  users: Observable<any> = this.http.get('/api/users');

  constructor(private http: HttpClient) {}

  ngOnInit() {
  }

}
