import { Company } from './../../_models/company';
import { PostsAddDialogComponent } from './../postsAddDialog/postsAddDialog.component';
import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer } from '@angular/forms';
import { Post, Address } from 'src/app/_models';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  @Input() company: Company;
  posts: Array<Post>;

  constructor(public controlContainer: ControlContainer,
              public dialog: MatDialog
              ) {}

  ngOnInit() {
    console.log('given company on init = ' + this.company);
  }

  openDialog() {
    console.log('given company on open dialog = ' + this.company);
    const newPost = new Post();
    newPost.address = this.company.address;
    newPost.company = this.company;

    console.log('data address = ' + newPost.address);
    console.log('data companie = ' + newPost.company.name);


    const dialogRef = this.dialog.open(PostsAddDialogComponent, {
      width: '75%',
      data: newPost
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.company.posts.push(result);
      console.log('result = ' + result);
    });
  }

}
