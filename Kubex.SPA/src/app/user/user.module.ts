import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import {MatInputModule} from '@angular/material/input';
import { FlexLayoutModule } from '@angular/flex-layout';

import { UsersRoutingModule } from './user-routing.module';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './edit/edit.component';
import { UserLayoutComponent } from './userLayout/userLayout.component';
import { AddressComponent } from '../address/address.component';

@NgModule({
    declarations: [
        UserLayoutComponent,
        ListComponent,
        AddEditComponent,
        AddressComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        FlexLayoutModule,
        MatInputModule
    ]
})
export class UserModule { }
