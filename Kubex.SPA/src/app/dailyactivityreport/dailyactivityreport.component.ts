import { DailyactivityreportService } from './../_services';
import { DailyActivityReport, Entry } from './../_models';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-dailyactivityreport',
  templateUrl: './dailyactivityreport.component.html',
  styleUrls: ['./dailyactivityreport.component.css']
})
export class DailyactivityreportComponent implements OnInit {

  constructor() {}

  ngOnInit() {}

}
