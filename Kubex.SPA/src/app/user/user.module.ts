import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';

import { UsersRoutingModule } from './user-routing.module';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './edit/edit.component';
import { UserLayoutComponent } from './userLayout/userLayout.component';
import { AddressComponent } from '../address/address.component';
import { ContactComponent } from '../contact/contact.component';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
    declarations: [
        UserLayoutComponent,
        ListComponent,
        AddEditComponent,
        AddressComponent,
        ContactComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        FlexLayoutModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule
    ]
})
export class UserModule { }
