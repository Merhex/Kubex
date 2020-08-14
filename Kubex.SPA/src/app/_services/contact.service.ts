import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../_models/contact';

@Injectable({
    providedIn: 'root'
})
export class ContactService {
    baseUrl = environment.apiUrl + '/contacts';

    constructor(private http: HttpClient) { }

    add(contact: Contact): Observable<Contact> {
        return this.http.post<Contact>(this.baseUrl + '/add', contact);
    }

    delete(id: number) {
        return this.http.delete(this.baseUrl + `/${id}`);
    }

    update(contact: Contact) {
        return this.http.patch(this.baseUrl + '/update', contact);
    }

    getContactsForUser(username: string): Observable<Contact[]> {
        return this.http.get<Contact[]>(this.baseUrl + `/user/${username}`);
    }

    getContactsForCompany(companyId: number): Observable<Contact[]> {
        return this.http.get<Contact[]>(this.baseUrl + `/company/${companyId}`);
    }
}
