import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { ButtonModule } from 'primeng/button';
import { ListboxModule } from 'primeng/listbox';
import { AccordionModule } from 'primeng/accordion';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { UserComponent } from './user/user.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { DailyactivityreportComponent } from './dailyactivityreport/dailyactivityreport.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      UserComponent,
      HomeComponent,
      LoginComponent,
      DailyactivityreportComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule,
      ButtonModule,
      ListboxModule,
      AccordionModule,
      FontAwesomeModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
