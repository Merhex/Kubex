import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Post, User } from '../_models';
import { PostService } from '../_services/post.service';
import { catchError } from 'rxjs/operators';
import { AlertService, AccountService } from '../_services';


@Injectable()
export class DarResolver implements Resolve<Post> {
    user: User;

    constructor(private router: Router,
                private postService: PostService,
                private alertService: AlertService,
                private accountService: AccountService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Post> {
        let postId;
        this.accountService.user.subscribe(u => {
            this.user = u;
        });

        if (route.paramMap.get('postId') == null) {
            postId = this.user.postIds[0];
        } else {
            // tslint:disable-next-line: radix
            postId = parseInt(route.paramMap.get('postId'));
        }

        return this.postService.get(postId).pipe(
            catchError(error => {
                this.router.navigate(['/']);
                this.alertService.info('You are not assigned to a post yet. Please contact your supervisor.');
                return of(null);
            })
        );
    }
}
