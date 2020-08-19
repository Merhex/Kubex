import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Post } from '../_models';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
export class PostService {

    baseUrl = environment.apiUrl + '/post/';

    constructor(private http: HttpClient) { }

    create(post: Post) {
        return this.http.post<Post>(this.baseUrl + 'create/', post);
    }

    update(post: Post) {
        return this.http.patch<Post>(this.baseUrl + `update/${post.id}`, post);
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
