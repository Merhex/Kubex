import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AccountService } from './account.service';
import { HttpClient } from '@angular/common/http';
import { Post } from '../_models';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
export class PostService {

    baseUrl = environment.apiUrl + '/post/';

    constructor(private accountService: AccountService,
                private http: HttpClient) { }

    create(params: Post) {
        return this.http.post<Post>(this.baseUrl + 'create/', params);
    }

    update() {
        // return this.http.patch<Post>(this.baseUrl + `update/${id}`, params);
    }

    get(id: number) {
        return this.http.get<Post>(this.baseUrl + `${id}`);
    }

    delete(id: number) {
        return this.http.delete(this.baseUrl + id);
    }

    getPostsForUser(userName: string): Observable<Post[]> {
        return this.http.get<Post[]>(this.baseUrl + `user/${userName}`);
    }
}
