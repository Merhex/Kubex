import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { DailyActivityReport, EntryAdd } from '../_models';
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

  getLastDar(postId: number): Observable<DailyActivityReport> {
    return this.http.get<DailyActivityReport>(this.baseUrl + `last/${postId}`);
  }

  getDarsByPost(postId: number): Observable<DailyActivityReport[]> {
    return this.http.get<DailyActivityReport[]>(this.baseUrl + `post/${postId}`);
  }

  createDar(postId: number): Observable<DailyActivityReport> {
    return this.http.post<DailyActivityReport>(this.baseUrl + `create/${postId}`, {});
  }

  addEntry(entry: EntryAdd): Observable<DailyActivityReport> {
    return this.http.post<DailyActivityReport>(this.baseUrl + 'add', entry);
  }

}
