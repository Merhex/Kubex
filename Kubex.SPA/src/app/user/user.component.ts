import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  // users: Observable<any> = this.http.get('http://localhost:3000/api/users');
  users: Observable<any> = this.http.get('/api/users');

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

}
