import { Company } from './../_models/company';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { CompanyRegister } from '../_models/companyRegister';
import { LogoUrl } from '../_models/logoUrl';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  baseUrl = environment.apiUrl + '/companies/';

  constructor(
    private router: Router,
    private http: HttpClient
  ) {}

  getCompanies(): Observable<Company[]> {
    return this.http.get<Company[]>(this.baseUrl);
  }

  getCompanyById(id: number): Observable<Company> {
    return this.http.get<Company>(this.baseUrl + id);
  }

  register(company: CompanyRegister) {
    return this.http.post(this.baseUrl + 'create/', company);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }

  uploadFile(formData: FormData): Observable<LogoUrl> {
    return this.http.post<LogoUrl>(this.baseUrl + 'upload/', formData);
  }
}
