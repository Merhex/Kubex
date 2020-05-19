import { UserRegister } from './../_models/userRegister';
import { User } from './../_models/user';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {
        return this.userSubject.value;
    }

    login(username, password) {
        return this.http.post<User>(`${environment.apiUrl}/auth/login`, { username, password })
            .pipe(map(user => {
                // Sla User en JWT token op in lokale storage: Houd u ingeloged bij refresh
                localStorage.setItem('user', JSON.stringify(user));
                this.userSubject.next(user);
                return user;
            }));
    }

    logout() {
        // Verwijder User uit lokale storage
        localStorage.removeItem('user');
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

    update(userName, params) {
        return this.http.put(`${environment.apiUrl}/users/${userName}`, params)
            .pipe(map(x => {
                // User in lokale storage updaten met nieuwe data
                if (userName === this.userValue.userName) {
                    const user = { ...this.userValue, ...params };
                    localStorage.setItem('user', JSON.stringify(user));

                    this.userSubject.next(user);
                }
                return x;
            }));
    }

    delete(userName: string) {
        return this.http.delete(`${environment.apiUrl}/users/${userName}`)
            .pipe(map(x => {
                // Wanneer hudige User wordt verwijderd, ook meteen uitloggen
                if (userName === this.userValue.userName) {
                    this.logout();
                }
                return x;
            }));
    }
}
