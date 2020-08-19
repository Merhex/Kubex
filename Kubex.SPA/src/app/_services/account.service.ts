import { UserRegister } from './../_models/userRegister';
import { User } from './../_models/user';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserToken } from '../_models/UserToken';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
    private token = new BehaviorSubject<string>(JSON.parse(localStorage.getItem('token')));
    public user = this.userSubject.asObservable();
    public jwtToken = this.token.asObservable();

    constructor(
        private router: Router,
        private http: HttpClient
    ) { }

    login(username, password) {
        return this.http.post<UserToken>(`${environment.apiUrl}/auth/login`, { username, password })
            .pipe(map((userToken: UserToken) => {
                // Sla User en JWT token op in lokale storage: Houd u ingeloged bij refresh
                localStorage.setItem('user', JSON.stringify(userToken.user));
                localStorage.setItem('token', JSON.stringify(userToken.token));
                this.userSubject.next(userToken.user);
                this.token.next(userToken.token);
                return userToken;
            }));
    }

    updateUser(user: User) {
        this.userSubject.next(user);
    }

    logout() {
        // Verwijder huidige User uit lokale storage
        localStorage.removeItem('user');
        localStorage.removeItem('token');

        this.userSubject.next(null);
        this.router.navigate(['/account/login']);
    }

    register(user: UserRegister) {
        return this.http.post(`${environment.apiUrl}/auth/register`, user);
    }

    getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/users`);
    }

    getByUserName(userName: string) {
        return this.http.get<User>(`${environment.apiUrl}/users/${userName}`);
    }

    getUsersFromPost(postId: number) {
        return this.http.get<User[]>(`${environment.apiUrl}/users/post/${postId}`);
    }

    update(userName, params) {
        return this.http.put(`${environment.apiUrl}/users/${userName}`, params)
            .pipe(map(x => {
                // User in lokale storage updaten met nieuwe data
                if (userName === this.userSubject.value.userName) {
                    const user = { ...this.userSubject.value, ...params };
                    localStorage.setItem('user', JSON.stringify(user));

                    this.userSubject.next(user);
                }
                return x;
            }));
    }

    delete(userName: string) {
        return this.http.delete(`${environment.apiUrl}/users/${userName}`)
            .pipe(map(x => {
                // Wanneer ingelogde User wordt verwijderd, ook meteen uitloggen
                if (userName === this.userSubject.value.userName) {
                    this.logout();
                }
                return x;
            }));
    }

    deleteUserFromPost(postId: number, userName: string) {
        return this.http.delete(`${environment.apiUrl}/users/post/${postId}/${userName}`);
    }
}
