import { Company } from './../../_models/company';
import { Post } from './../../_models/post';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-postsadddialog',
  templateUrl: './postsAddDialog.component.html',
  styleUrls: ['./postsAddDialog.component.css']
})
export class PostsAddDialogComponent implements OnInit {
  postForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<PostsAddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Post
              ) {}

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      postName: '',
      address: ''
    });
    console.log('company = ' + this.data.company);
    console.log('name =' + this.data.name);
  }

  submit(postForm) {
    this.data.name = postForm.value.postName;
    console.log(this.data.name);
    // this.dialogRef.close(this.data);
    this.dialogRef.close(`${postForm.value.postName}`);
  }

  close(): void {
    this.dialogRef.close();
  }
}
