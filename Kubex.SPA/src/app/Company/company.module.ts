import { EditComponent } from './edit/edit.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { CompanyLayoutComponent } from './companyLayout/companyLayout.component';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CompanyRoutingModule } from './company-routing.module';

@NgModule({
    declarations: [
        CompanyLayoutComponent,
        CompanyListComponent,
        EditComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        CompanyRoutingModule
    ]
})
export class CompanyModule { }
