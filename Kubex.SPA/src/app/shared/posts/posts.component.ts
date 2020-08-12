import { Company } from './../../_models/company';
import { PostsAddDialogComponent } from './../postsAddDialog/postsAddDialog.component';
import { Component, Input } from '@angular/core';
import { ControlContainer } from '@angular/forms';
import { Post, Address } from 'src/app/_models';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent {
  @Input() company: Company;
  posts: Array<Post>;

  constructor(public controlContainer: ControlContainer,
              public dialog: MatDialog
              ) {}

  openDialog() {
    console.log(this.company);
    const newPost = new Post();
    newPost.address = new Address();
    newPost.company = this.company;


    const dialogRef = this.dialog.open(PostsAddDialogComponent, {
      width: '75%',
      data: newPost
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.company.posts.push(result);
    });
  }

}
