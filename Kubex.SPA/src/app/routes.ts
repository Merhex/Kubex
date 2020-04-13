import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { DailyactivityreportComponent } from './dailyactivityreport/dailyactivityreport.component';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'users', component: UserComponent },
    { path: 'dar', component: DailyactivityreportComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
