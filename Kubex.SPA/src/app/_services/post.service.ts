import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AccountService } from './account.service';
import { HttpClient } from '@angular/common/http';
import { Post } from '../_models';

@Injectable({
    providedIn: 'root'
  })
export class PostService {

    constructor(private accountService: AccountService,
                private http: HttpClient) { }

    create(params: Post) {
        return this.http.post<Post>(`${environment.apiUrl}/post/create`, params);
    }

    update() {

    }

    get() {

    }

    delete() {

    }
}
