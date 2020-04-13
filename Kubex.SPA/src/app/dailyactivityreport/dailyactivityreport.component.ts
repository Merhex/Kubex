import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dailyactivityreport',
  templateUrl: './dailyactivityreport.component.html',
  styleUrls: ['./dailyactivityreport.component.css']
})
export class DailyactivityreportComponent implements OnInit {

  entries: Observable<any> = this.http.get('http://localhost:3000/entry/');


  constructor(private http: HttpClient) {}

  ngOnInit() {
  }

}
