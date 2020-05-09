import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Entry } from '../_models/entry';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-dailyactivityreport',
  templateUrl: './dailyactivityreport.component.html',
  styleUrls: ['./dailyactivityreport.component.css']
})
export class DailyactivityreportComponent implements OnInit {

  entries: Observable<Entry[]> = this.http.get<Entry[]>('http://localhost:3000/entry/');
  detail: Observable<Entry>;
  postEntry: FormGroup;
  postSubEntry: FormGroup;


  constructor(private http: HttpClient) {}

  ngOnInit() {
  }

  GetEntries(): Observable<Entry[]> {
    return this.http.get<Entry[]>('http://localhost:3000/entry/');
  }

  GetDetail(parentId: BigInteger) {
    return this.http.get<Entry[]>('http://localhost:3000/entry/');
  }

  SendDetail() {
    this.http.post('http://localhost:3000/entry/', {
      id: 5,
      parentid: null,
      occuranceDate: '2020-04-13T07:17:35.511Z',
      location: null,
      description: 'This is a first test',
      priorityId: 0,
      creauserid: 1,
      creatime: Date.now,
      moduserid: 1,
      modtime: Date.now,
      mediacolledctionid: null
    }
    );
  }

  onSubmit() {}

  onSubmitSub() {}

}
