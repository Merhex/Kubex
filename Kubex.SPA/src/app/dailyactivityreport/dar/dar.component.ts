import { DailyactivityreportService } from 'src/app/_services';
import { DailyActivityReport, Entry } from 'src/app/_models';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-dar',
  templateUrl: './dar.component.html',
  styleUrls: ['./dar.component.css']
})
export class DarComponent implements OnInit {
  postEntry: FormGroup;
  postSubEntry: FormGroup;
  dar: DailyActivityReport;
  date: Date;
  id: number;
  entries: Entry[];
  detail: Observable<Entry>;


  constructor(private dailyactivityreportService: DailyactivityreportService) {
    this.dar = new DailyActivityReport();

    // Probeer de DAR op datum van vandaag op te halen
    this.dailyactivityreportService.getLastDar()
      .subscribe(dar => {
        this.dar = dar;
        this.entries = dar.entries as Entry[];
    });
  }

  ngOnInit() {}

  onSubmit() {}

  onSubmitSub() {}

  createDar() {
    console.log('clickedyclik');
    this.dailyactivityreportService.createDar()
      .subscribe(dar => {
        this.dar = dar;
        this.entries = dar.entries as Entry[];
    });
  }

  // GetDetail(parentId: BigInteger) {
  //   return this.http.get<Entry[]>('http://localhost:3000/entry/');
  // }

  // SendDetail() {
  //   this.http.post('http://localhost:3000/entry/', {
  //     id: 5,
  //     parentid: null,
  //     occuranceDate: '2020-04-13T07:17:35.511Z',
  //     location: null,
  //     description: 'This is a first test',
  //     priorityId: 0,
  //     creauserid: 1,
  //     creatime: Date.now,
  //     moduserid: 1,
  //     modtime: Date.now,
  //     mediacolledctionid: null
  //   }
  //   );
  // }

}
