import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DailyactivityreportService } from 'src/app/_services';
import { DailyActivityReport, Entry, EntryAdd, Location } from 'src/app/_models';

@Component({
  selector: 'app-subentry',
  templateUrl: './subEntry.component.html',
  styleUrls: ['./subEntry.component.css']
})
export class SubEntryComponent implements OnInit {
  @Input() entry: Entry;
  @Input() dar: DailyActivityReport;
  postSubEntry: FormGroup;

  constructor(
    private dailyactivityreportService: DailyactivityreportService,
    private formBuilder: FormBuilder
  ) {}

    // Convenience getter voor de formulier velden
    get s() { return this.postSubEntry.controls; }

  ngOnInit() {
    this.postSubEntry = this.formBuilder.group({
      subEntryTime: [''],
      subEntryLocation: [''],
      subEntryDescription: ['']
    });
  }

  onSubmit() {
    // Zet ingegeven tijd om naar DateTime object
    const data = this.s.entryTime.value;
    const hours = data.substring(0, 2);
    const minutes = data.substring(3);

    const time = new Date();
    time.setHours(hours, minutes, 0);

    // Maak de entry klaar voor verzenden
    const subEntry = new Entry();
    const subEntryAdd = new EntryAdd();
    const location = new Location();

    location.name = this.s.entryLocation.value;

    subEntry.occuranceDate = time;
    subEntry.description = this.s.entryDescription.value;
    subEntry.priority = 'Low';
    subEntry.entryType = 'Log';
    subEntry.location = location;

    subEntryAdd.DailyActivityReport = this.dar;
    subEntryAdd.parentEntry = this.entry;
    subEntryAdd.entry = subEntry;

    console.log('loc:' + location.name);
    console.log('des:' + subEntry.description);
    console.log('tim:' + subEntry.occuranceDate);

    // Voeg een entry in het dar in
    this.dailyactivityreportService.addEntry(subEntryAdd)
      .subscribe((dar: DailyActivityReport) => {
        this.dar = dar;
        // this.entries = dar.entries as Entry[];
        console.log(dar);
    });
  }

}
