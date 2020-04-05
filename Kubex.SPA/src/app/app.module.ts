import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { UserComponent } from './user/user.component';
import { UserlistComponent } from './user/userlist/userlist.component';
import { UserdetailComponent } from './user/userdetail/userdetail.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

import { ButtonModule } from 'primeng/button';
import {ListboxModule} from 'primeng/listbox';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      UserComponent,
      UserlistComponent,
      UserdetailComponent,
      HomeComponent,
      LoginComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule,
      ButtonModule,
      ListboxModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
