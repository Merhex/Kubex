import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AccountService } from '../_services/index';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            // Om niet te moeten zoeken, de volledige error meteen uitprinten in de console
            console.log(err);

            if (err.status === 401) {
                // Wanneer we 401 'Unauthorized' ontvangen van de API, meteen ook uitloggen
                this.accountService.logout();
            }

            // We tonen de boodschap binnen de fout, indien niet beschikbaar tonen we de statustext
            const error = err.error.error || err.statusText;
            return throwError(error);
        }));
    }
}
