import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Entry } from '../_models/Entry';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DailyactivityreportService {

constructor(private http: HttpClient) {}

getEntries() {

}

}
