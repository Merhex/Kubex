import { environment } from './../environments/environment';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';

import { ButtonModule } from 'primeng/button';
import { ListboxModule } from 'primeng/listbox';
import { AccordionModule } from 'primeng/accordion';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { AlertComponent } from './alert/alert.component';
import { AccountComponent } from './account/account.component';
import { HomeComponent } from './home/home.component';
import { DailyactivityreportComponent } from './dailyactivityreport/dailyactivityreport.component';

import {MatExpansionModule} from '@angular/material/expansion';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { MatGoogleMapsAutocompleteModule } from '@angular-material-extensions/google-maps-autocomplete';
import { AgmCoreModule } from '@agm/core';

@NgModule({
   declarations: [
      AppComponent,
      UserComponent,
      HomeComponent,
      DailyactivityreportComponent,
      AlertComponent,
      AccountComponent
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
      MatMenuModule,
      ScrollingModule,
      MatGoogleMapsAutocompleteModule,
      AgmCoreModule.forRoot({
         apiKey: environment.mapKey,
         libraries: ['places']
      })
   ],
   providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
