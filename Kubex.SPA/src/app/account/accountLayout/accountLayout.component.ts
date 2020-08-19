import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services';
import { User } from 'src/app/_models';

@Component({ templateUrl: 'accountLayout.component.html' })
export class AccountLayoutComponent {
    user: User;

    constructor(
        private router: Router,
        private accountService: AccountService
    ) {
        // Naar home verwijzen indien ingeloged
        this.accountService.user.subscribe(user => {
            this.user = user;
        });

        if (this.user) {
            this.router.navigate(['/']);
        }
    }
}
