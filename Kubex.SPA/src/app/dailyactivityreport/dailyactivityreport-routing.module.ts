import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DailyactivityreportLayoutComponent } from './dailyactivityreportLayout/dailyactivityreportLayout.component';
import { DarComponent } from './dar/dar.component';

const routes: Routes = [
    {
        path: '', component: DailyactivityreportLayoutComponent,
        children: [
            { path: '', component: DarComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DailyactivityreportRoutingModule { }
