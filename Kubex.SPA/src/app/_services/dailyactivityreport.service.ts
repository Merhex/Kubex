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
  baseUrl = environment.apiUrl + '/dar/';

  constructor(private http: HttpClient) {}

  getDarById(id: number): Observable<DailyActivityReport> {
    return this.http.get<DailyActivityReport>(this.baseUrl + id);
  }

  getLastDar(): Observable<DailyActivityReport> {
    return this.http.get<DailyActivityReport>(this.baseUrl + 'last');
  }

  createDar(): Observable<DailyActivityReport> {
    return this.http.post<DailyActivityReport>(this.baseUrl + 'create', {});
  }

}
