import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { DailyActivityReport } from '../_models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DailyactivityreportService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getDar(id: number): Observable<DailyActivityReport> {
    return this.http.get<DailyActivityReport>(this.baseUrl + 'dailyactivityreport/' + id);
  }

}
