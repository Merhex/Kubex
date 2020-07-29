import { SharedModule } from './../shared/shared.module';
import { EditComponent } from './edit/edit.component';
import { CompanyListComponent } from './companyList/companyList.component';
import { CompanyLayoutComponent } from './companyLayout/companyLayout.component';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CompanyRoutingModule } from './company-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

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
        CompanyRoutingModule,
        FlexLayoutModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        SharedModule
    ]
})
export class CompanyModule { }
