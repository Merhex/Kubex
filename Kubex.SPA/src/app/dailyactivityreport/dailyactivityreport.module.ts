import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { DailyactivityreportLayoutComponent } from './dailyactivityreportLayout/dailyactivityreportLayout.component';
import { DailyactivityreportRoutingModule } from './dailyactivityreport-routing.module';
import { DarComponent } from './dar/dar.component';

import {MatExpansionModule} from '@angular/material/expansion';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {FlexLayoutModule} from '@angular/flex-layout';
import {MatMenuModule} from '@angular/material/menu';

@NgModule({
  declarations: [
    DailyactivityreportLayoutComponent,
    DarComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DailyactivityreportRoutingModule,
    MatExpansionModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ScrollingModule,
    FlexLayoutModule,
    MatMenuModule
  ]
})
export class DailyactivityreportModule { }
