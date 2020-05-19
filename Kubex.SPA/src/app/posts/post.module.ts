import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostMainComponent } from './post-main/posts.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostCreateComponent } from './post-create/post-create.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { PostRoutingModule } from './post-routing.module';
import { PostComponent } from './PostComponent';

@NgModule({
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    PostRoutingModule
  ],
  declarations: [
    PostMainComponent,
    PostCreateComponent,
    PostComponent
  ]
})
export class PostModule { }
