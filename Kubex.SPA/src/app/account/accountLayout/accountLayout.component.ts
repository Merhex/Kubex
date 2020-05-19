import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services';

@Component({ templateUrl: 'accountLayout.component.html' })
export class AccountLayoutComponent {
    constructor(
        private router: Router,
        private accountService: AccountService
    ) {
        // Naar home verwijzen indien ingeloged
        if (this.accountService.userValue) {
            this.router.navigate(['/']);
        }
    }
}
