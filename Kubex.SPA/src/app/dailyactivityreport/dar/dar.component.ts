import { DailyactivityreportService } from 'src/app/_services';
import { DailyActivityReport, Entry, EntryAdd, Location } from 'src/app/_models';
import { Component, OnInit } from '@angular/core';
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
  dar = new DailyActivityReport();
  date: Date;
  id: number;
  entries: Entry[];
  detail: Observable<Entry>;


  constructor(
    private dailyactivityreportService: DailyactivityreportService,
    private formBuilder: FormBuilder
  ) {}

  // Convenience getter voor de formulier velden
  get e() { return this.postEntry.controls; }
  get s() { return this.postSubEntry.controls; }

  ngOnInit() {
    // Haal de laatste DAR op
    this.dailyactivityreportService.getLastDar()
      .subscribe((dar: DailyActivityReport) => {
        this.dar = dar;
        this.entries = dar.entries as Entry[];
        console.log(dar);
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

    console.log('id = ' + this.dar.id);
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
      .subscribe((dar: DailyActivityReport) => {
        this.dar = dar;
        this.entries = dar.entries as Entry[];
        console.log(dar);
    });
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

    console.log('loc:' + location.name);
    console.log('des:' + subEntry.description);
    console.log('tim:' + subEntry.occuranceDate);

    // Voeg een entry in het dar in
    this.dailyactivityreportService.addEntry(subEntryAdd)
      .subscribe((dar: DailyActivityReport) => {
        this.dar = dar;
        this.entries = dar.entries as Entry[];
        console.log(dar);
    });
  }

  createDar() {
    this.dailyactivityreportService.createDar()
      .subscribe(dar => {
        this.dar = dar;
        this.entries = dar.entries as Entry[];
    });
  }

}
