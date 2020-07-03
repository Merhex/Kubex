import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AccountService, AlertService } from 'src/app/_services';
import { User } from 'src/app/_models';
import { HttpErrorResponse } from '@angular/common/http';

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
        console.log(userName);
        const user = this.users.find(x => x.userName === userName);
        user.isDeleting = true;
        this.accountService.delete(userName)
            .pipe(first())
            .subscribe(() => {
                this.users = this.users.filter(x => x.userName !== userName);
                this.alertService.success(userName + ' deleted succesfully!');
            },
            (error: HttpErrorResponse) => {
                this.alertService.error('You are not allowed to delete a user.');
                user.isDeleting = false;
            });
    }
}
