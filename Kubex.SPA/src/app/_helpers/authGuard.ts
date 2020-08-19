import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AccountService } from '../_services/index';
import { User } from '../_models';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    user: User;

    constructor(
        private router: Router,
        private accountService: AccountService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        this.accountService.user.subscribe(user => {
            this.user = user;
        });

        if (this.user) {
            return true;
        }

        // Wanneer niet ingeloged, doorverwijzen naar login page
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
