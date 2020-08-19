import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers';
import { HomeResolver } from './_resolvers/home.resolver';
import { DarResolver } from './_resolvers/dar.resolver';
import { RefreshComponent } from './refresh/refresh.component';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./user/user.module').then(x => x.UserModule);
const dailyactivityreportModule = () => import('./dailyactivityreport/dailyactivityreport.module').then(x => x.DailyactivityreportModule);
const companyModule = () => import('./company/company.module').then(x => x.CompanyModule);

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always'  },
    { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard], runGuardsAndResolvers: 'always'  },
    { path: 'account', loadChildren: accountModule },
    { path: 'dar', loadChildren: dailyactivityreportModule,
        canActivate: [AuthGuard],
        resolve: { post: DarResolver},
        runGuardsAndResolvers: 'always' },
    { path: 'companies', loadChildren: companyModule, canActivate: [AuthGuard], runGuardsAndResolvers: 'always' },
    { path: 'refresh', component: RefreshComponent },

    // Alle andere paden verwijzen naar Home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
    exports: [RouterModule]
})
export class AppRoutingModule { }
