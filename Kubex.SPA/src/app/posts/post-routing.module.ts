import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostMainComponent } from './post-main/posts.component';
import { PostCreateComponent } from './post-create/post-create.component';
import { PostComponent } from './PostComponent';

const routes: Routes = [
    {
        path: '', component: PostComponent,
        children: [
            { path: '', component: PostMainComponent },
            { path: 'create', component: PostCreateComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PostRoutingModule { }
