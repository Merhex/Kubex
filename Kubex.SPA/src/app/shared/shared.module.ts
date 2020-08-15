import { PostsAddDialogComponent } from './postsAddDialog/postsAddDialog.component';
import { SharedComponent } from './shared.component';
import { ContactComponent } from './contact/contact.component';
import { AddressComponent } from './address/address.component';
import { PostsComponent } from './posts/posts.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersRoutingModule } from '../user/user-routing.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatListModule } from '@angular/material/list';

@NgModule({
  imports: [
    CommonModule,
        FormsModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        FlexLayoutModule,
        MatInputModule,
        MatSelectModule,
        MatIconModule,
        MatButtonModule,
        MatDialogModule,
        MatCardModule,
        MatAutocompleteModule,
        MatListModule
  ],
  declarations: [
    SharedComponent,
    AddressComponent,
    ContactComponent,
    PostsComponent,
    PostsAddDialogComponent
  ],
  exports: [
    AddressComponent,
    ContactComponent,
    PostsComponent
  ],
  entryComponents: [PostsAddDialogComponent]
})
export class SharedModule { }
