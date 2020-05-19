import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './user-routing.module';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './edit/edit.component';
import { UserLayoutComponent } from './userLayout/userLayout.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        UsersRoutingModule
    ],
    declarations: [
        UserLayoutComponent,
        ListComponent,
        AddEditComponent
    ]
})
export class UserModule { }
