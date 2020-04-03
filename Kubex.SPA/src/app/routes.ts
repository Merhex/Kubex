import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
// import { ClientComponent } from './client/client.component';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    // { path: 'clients', component: ClientComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];