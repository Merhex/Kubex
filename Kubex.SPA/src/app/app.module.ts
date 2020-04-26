import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
import { SubentryComponent } from './dailyactivityreport/subentry/subentry.component';

import {MatExpansionModule} from '@angular/material/expansion';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {ScrollingModule} from '@angular/cdk/scrolling';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      UserComponent,
      HomeComponent,
      LoginComponent,
      DailyactivityreportComponent,
      SubentryComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule,
      ButtonModule,
      FlexLayoutModule,
      ListboxModule,
      AccordionModule,
      FontAwesomeModule,
      BrowserAnimationsModule,
      MatExpansionModule,
      MatInputModule,
      MatButtonModule,
      MatIconModule,
      ScrollingModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
