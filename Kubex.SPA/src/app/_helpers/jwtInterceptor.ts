import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountService } from '../_services/index';
import { User } from '../_models';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    token: string;
    user: User;

    constructor(private accountService: AccountService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.accountService.user.subscribe(user => {
            this.user = user;
        });
        this.accountService.jwtToken.subscribe(token => {
            this.token = token;
        });

        const isLoggedIn = this.user && this.token;
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.token}`
                }
            });
        }

        return next.handle(request);
    }
}
