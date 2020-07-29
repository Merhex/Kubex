import { ContactComponent } from './contact/contact.component';
import { AddressComponent } from './address/address.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedComponent } from './shared.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersRoutingModule } from '../user/user-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  imports: [
    CommonModule,
        FormsModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        FlexLayoutModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule
  ],
  declarations: [
    SharedComponent,
    AddressComponent,
    ContactComponent
  ],
  exports: [
    AddressComponent,
    ContactComponent
  ]
})
export class SharedModule { }
