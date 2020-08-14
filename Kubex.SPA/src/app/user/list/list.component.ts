import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AccountService, AlertService } from 'src/app/_services';
import { User } from 'src/app/_models';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    users = Array<User>();

    constructor(
        private accountService: AccountService,
        private alertService: AlertService) {}

    ngOnInit() {
        this.accountService.getAll()
            .pipe(first())
            .subscribe(users => this.users = users);
    }

    deleteUser(userName: string) {
        const user = this.users.find(x => x.userName === userName);
        user.isDeleting = true;
        this.accountService.delete(userName)
            .pipe(first())
            .subscribe(() => {
                this.alertService.success(`${userName} has been removed!`);
                this.users = this.users.filter(x => x.userName !== userName);
            },
            error => {
                this.alertService.error(error);
            });
    }
}
