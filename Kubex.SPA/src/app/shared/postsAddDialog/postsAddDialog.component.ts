import { Company } from './../../_models/company';
import { Post } from './../../_models/post';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-postsadddialog',
  templateUrl: './postsAddDialog.component.html',
  styleUrls: ['./postsAddDialog.component.css']
})
export class PostsAddDialogComponent implements OnInit {
  postForm: FormGroup;

  // Convenience getter voor de formulier velden
  get f() { return this.postForm.controls; }

  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<PostsAddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Post
              ) {}

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      postName: '',
      street: [this.data.company.address.street, Validators.required],
      houseNumber: [this.data.company.address.houseNumber, Validators.required],
      appartementBus: [this.data.company.address.appartementBus],
      zip: [this.data.company.address.zip, Validators.required],
      country: [this.data.company.address.country, Validators.required],
    });
  }

  submit(postForm) {
    this.data.name = postForm.value.postName;
    console.log(this.data.name);
    this.dialogRef.close(this.data);
  }

  close(): void {
    this.dialogRef.close();
  }
}
