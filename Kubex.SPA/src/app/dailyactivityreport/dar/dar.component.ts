import { DailyactivityreportService, AlertService, AccountService } from 'src/app/_services';
import { DailyActivityReport, Entry, EntryAdd, Location } from 'src/app/_models';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardHeader } from '@angular/material/card';

@Component({
  selector: 'app-dar',
  templateUrl: './dar.component.html',
  styleUrls: ['./dar.component.css']
})
export class DarComponent implements OnInit {
  hasPosts = false;
  lastDarId: number;
  isDisabledNext: boolean;
  isDisabledPrevious: boolean;
  postEntry: FormGroup;
  postSubEntry: FormGroup;
  dar = new DailyActivityReport();
  date: Date;
  id: number;
  postId: number;
  entries: Entry[];
  detail: Observable<Entry>;


  constructor(
    private dailyactivityreportService: DailyactivityreportService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private accountService: AccountService
  ) {}

  // Convenience getter voor de formulier velden
  get e() { return this.postEntry.controls; }
  get s() { return this.postSubEntry.controls; }

  ngOnInit() {
    const user = this.accountService.userValue as any;
    const postIds = user.user.postIds as number[];

    if (!postIds) {
      return;
    }

    this.postId = postIds[postIds.length - 1];
    console.log(this.postId);

    // Haal de laatste DAR op
    this.dailyactivityreportService.getDarsByPost(postIds[postIds.length - 1])
      .subscribe(
        (dars: DailyActivityReport[]) => {
          const lastDar = dars[dars.length - 1];

          this.dar = lastDar;
          this.entries = lastDar.entries as Entry[];
          this.lastDarId = lastDar.id;
          this.alertService.clear();
          // Deactiveer buttons
          this.isDisabledNext = true;
          if (dars.find(x => x.id === Math.min.apply(Math, dars.map((d: DailyActivityReport) => {
            return x.id;
          })))) {
            this.isDisabledPrevious = true;
          }
        },
        error => {
          this.alertService.error(error);
    });

    // Set forms
    this.postEntry = this.formBuilder.group({
      entryTime: [''],
      entryLocation: [''],
      entryDescription: ['']
    });
    this.postSubEntry = this.formBuilder.group({
      subEntryTime: [''],
      subEntryLocation: [''],
      subEntryDescription: ['']
    });
  }

  onSubmit() {
    // Zet ingegeven tijd om naar DateTime object
    const data = this.e.entryTime.value;
    const hours = data.substring(0, 2);
    const minutes = data.substring(3);

    const time = new Date();
    time.setHours(hours, minutes, 0);

    // Maak de entry klaar voor verzenden
    const entry = new Entry();
    const entryAdd = new EntryAdd();
    const location = new Location();

    location.name = this.e.entryLocation.value;

    entry.occuranceDate = time;
    entry.location = this.e.entryLocation.value;
    entry.description = this.e.entryDescription.value;
    entry.priority = 'Low';
    entry.entryType = 'Log';
    entry.location = location;

    entryAdd.DailyActivityReport = this.dar;
    entryAdd.parentEntry = null;
    entryAdd.entry = entry;

    // Voeg een entry in het dar in
    this.dailyactivityreportService.addEntry(entryAdd)
      .subscribe(
        (dar: DailyActivityReport) => {
          this.dar = dar;
          this.entries = dar.entries as Entry[];
          this.alertService.clear();
        },
        error => {
          this.alertService.error(error);
    });

    this.postEntry.reset();
  }

  onSubmitSub(parentEntry: Entry) {
    // Zet ingegeven tijd om naar DateTime object
    const data = this.s.subEntryTime.value;
    const hours = data.substring(0, 2);
    const minutes = data.substring(3);

    const time = new Date();
    time.setHours(hours - 2, minutes, 0);

    // Maak de entry klaar voor verzenden
    const subEntry = new Entry();
    const subEntryAdd = new EntryAdd();
    const location = new Location();

    location.name = this.s.subEntryLocation.value;

    subEntry.occuranceDate = time;
    subEntry.description = this.s.subEntryDescription.value;
    subEntry.priority = 'Low';
    subEntry.entryType = 'Log';
    subEntry.location = location;

    subEntryAdd.DailyActivityReport = this.dar;
    subEntryAdd.parentEntry = parentEntry;
    subEntryAdd.entry = subEntry;

    // Voeg een entry in het dar in
    this.dailyactivityreportService.addEntry(subEntryAdd)
      .subscribe(
        (dar: DailyActivityReport) => {
          this.dar = dar;
          this.entries = dar.entries as Entry[];
          this.alertService.clear();
        },
        error => {
          this.alertService.error(error);
    });

    this.postSubEntry.reset();
  }

  createDar() {
    this.dailyactivityreportService.createDar(this.postId)
      .subscribe(
        (dar: DailyActivityReport) => {
          this.dar = dar;
          this.entries = dar.entries as Entry[];
          this.alertService.clear();
          this.lastDarId = dar.id;
          this.isDisabledNext = true;
        },
        error => {
          this.alertService.error(error);
    });
  }

  gotoPreviousDar() {
    this.isDisabledNext = false;
    this.isDisabledPrevious = false;

    this.dailyactivityreportService.getDarById(this.dar.id - 1)
      .subscribe(
        (dar: DailyActivityReport) => {
          this.dar = dar;
          this.entries = dar.entries as Entry[];
          this.alertService.clear();
          if (dar.id === 1) { this.isDisabledPrevious = true; }
        },
        error => {
          this.alertService.error(error);
    });
  }

  gotoNextDar() {
    this.isDisabledNext = false;
    this.isDisabledPrevious = false;

    this.dailyactivityreportService.getDarById(this.dar.id + 1)
      .subscribe(
        (dar: DailyActivityReport) => {
          this.dar = dar;
          this.entries = dar.entries as Entry[];
          this.alertService.clear();
          if (dar.id === this.lastDarId) { this.isDisabledNext = true; }
        },
        error => {
          this.alertService.error(error);
    });
  }

  gotoTodaysDar() {
    this.isDisabledNext = false;
    this.isDisabledPrevious = false;

    this.dailyactivityreportService.getDarById(this.lastDarId)
      .subscribe(
        (dar: DailyActivityReport) => {
          this.dar = dar;
          this.entries = dar.entries as Entry[];
          this.alertService.clear();
          if (dar.id === this.lastDarId) { this.isDisabledNext = true; }
        },
        error => {
          this.alertService.error(error);
    });
  }

}
