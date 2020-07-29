import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./user/user.module').then(x => x.UserModule);
const dailyactivityreportModule = () => import('./dailyactivityreport/dailyactivityreport.module').then(x => x.DailyactivityreportModule);
const postModule = () => import('./posts/post.module').then(x => x.PostModule);
const companyModule = () => import('./company/company.module').then(x => x.CompanyModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard] },
    { path: 'account', loadChildren: accountModule },
    { path: 'dar', loadChildren: dailyactivityreportModule, canActivate: [AuthGuard] },
    { path: 'posts', loadChildren: postModule, canActivate: [AuthGuard] },
    { path: 'companies', loadChildren: companyModule, canActivate: [AuthGuard] },

    // Alle andere paden verwijzen naar Home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
