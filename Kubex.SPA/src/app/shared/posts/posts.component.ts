import { Company, Post } from 'src/app/_models';
import { PostsAddDialogComponent } from './../postsAddDialog/postsAddDialog.component';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ControlContainer } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  @Input() company: Company;
  @Output() companyChange = new EventEmitter<Post>();
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
      this.companyChange.emit(result);
    });
  }

}
