import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserToken } from '../_models/UserToken';
import { AccountService } from '../_services';
import { Observable, of, pipe } from 'rxjs';
import { catchError } from 'rxjs/operators';

export class HomeResolver implements Resolve<UserToken> {

    constructor(private accountService: AccountService,
                private router: Router) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): UserToken | Observable<UserToken> | Promise<UserToken> {
        throw new Error('Method not implemented.');
    }
}
