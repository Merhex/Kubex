import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyLayoutComponent } from './companyLayout/companyLayout.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
    {
        path: '', component: CompanyLayoutComponent,
        children: [
            { path: '', component: CompanyListComponent },
            { path: 'add', component: EditComponent },
            { path: 'edit/:username', component: EditComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CompanyRoutingModule {}
