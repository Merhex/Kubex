import { Company, Post } from 'src/app/_models';
import { PostsAddDialogComponent } from './../postsAddDialog/postsAddDialog.component';
import { Component, Input, OnInit, Output, EventEmitter, ChangeDetectorRef, OnChanges } from '@angular/core';
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
              public changeDetection: ChangeDetectorRef,
              public dialog: MatDialog
              ) {}

  ngOnInit() {
    this.posts = this.company.posts;
  }

  openDialog() {
    const newPost = new Post();
    newPost.address = this.company.address;
    newPost.company = this.company;


    const dialogRef = this.dialog.open(PostsAddDialogComponent, {
      width: '75%',
      data: newPost
    });

    dialogRef.afterClosed().subscribe(result => {
      this.company.posts.push(result);
      this.companyChange.emit(result);
    });
  }
}
