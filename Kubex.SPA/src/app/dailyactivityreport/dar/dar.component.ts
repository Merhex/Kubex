import { DailyactivityreportService, AlertService, AccountService } from 'src/app/_services';
import { DailyActivityReport, Entry, EntryAdd, Location, User } from 'src/app/_models';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dar',
  templateUrl: './dar.component.html',
  styleUrls: ['./dar.component.scss']
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
  postId: number;
  entries: Entry[];
  detail: Observable<Entry>;
  user: User;

  constructor(
    private dailyactivityreportService: DailyactivityreportService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private accountService: AccountService,
    private route: ActivatedRoute
  ) {}

  // Convenience getter voor de formulier velden
  get e() { return this.postEntry.controls; }
  get s() { return this.postSubEntry.controls; }

  ngOnInit() {
    // Haal User en Posten op uit Local Storage
    this.accountService.user.subscribe(user => {
      this.user = user;
    });
    const postIds = this.user.postIds as number[];
    // const user = this.accountService.userValue as any;
    // const postIds = user.user.postIds as number[];

    if (!postIds) { return; }

    this.route.params.subscribe(
      data => {
        if (data.postId) {
          // tslint:disable-next-line: radix
          const postId = parseInt(data.postId);

          console.log(postId);
          console.log(postIds);
          console.log(postIds.includes(postId));

          if (postIds.includes(postId)) {
            this.postId = postId;
          }
        } else {
          this.postId = postIds[0];
        }
      }
    );

    // Vul velden van formulieren in
    this.postEntry = this.formBuilder.group({
      entryTime: ['', Validators.required],
      entryLocation: ['', Validators.required],
      entryDescription: ['', Validators.required]
    });
    this.postSubEntry = this.formBuilder.group({
      subEntryTime: ['', Validators.required],
      subEntryLocation: ['', Validators.required],
      subEntryDescription: ['', Validators.required]
    });

    // Haal de laatste DAR op
    this.gotoTodaysDar();
    // this.dailyactivityreportService.getDarsByPost(postIds[postIds.length - 1])
    //   .subscribe(
    //     (dars: DailyActivityReport[]) => {
    //       const lastDar = dars[dars.length - 1];

    //       this.dar = lastDar;
    //       this.entries = lastDar.entries as Entry[];
    //       this.lastDarId = lastDar.id;
    //       this.alertService.clear();
    //       // Deactiveer buttons
    //       this.isDisabledNext = true;
    //       if (dars.find(x => x.id === Math.min.apply(Math, dars.map((d: DailyActivityReport) => {
    //         return x.id;
    //       })))) {
    //         this.isDisabledPrevious = true;
    //       }
    //     },
    //     error => {
    //       this.alertService.error(error);
    // });
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
    time.setHours(hours, minutes, 0);

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

    this.dailyactivityreportService.getDarsByPost(this.postId)
      .subscribe(
        (dars: DailyActivityReport[]) => {
          const index = dars.findIndex(d => d.id === this.dar.id);
          if (index - 1 === 0) {
            this.isDisabledPrevious = true;
          }

          this.dar = dars[index - 1];
          this.entries = this.dar.entries as Entry[];
          this.alertService.clear();
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

    console.log(this.postId);


    this.dailyactivityreportService.getDarsByPost(this.postId)
      .subscribe(
        (dars: DailyActivityReport[]) => {
          this.dar = dars[dars.length - 1];
          this.entries = this.dar.entries as Entry[];
          this.alertService.clear();

          if (this.dar.id === dars[dars.length - 1].id) { this.isDisabledNext = true; }
          if (dars.length === 1) { this.isDisabledPrevious = true; }
        },
        error => {
          this.alertService.error(error);
    });
  }

}
