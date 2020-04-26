import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-subentry',
  templateUrl: './subentry.component.html',
  styleUrls: ['./subentry.component.css']
})
export class SubentryComponent implements OnInit {

  entries: Observable<any> = this.http.get('http://localhost:3000/entry/');


  constructor(private http: HttpClient) {}

  ngOnInit() {
  }

}
