import { AccountService } from './../../_services/account.service';
import { Company } from './../../_models/company';
import { Post } from './../../_models/post';
import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { User } from 'src/app/_models';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-postsadddialog',
  templateUrl: './postsAddDialog.component.html',
  styleUrls: ['./postsAddDialog.component.css']
})
export class PostsAddDialogComponent implements OnInit {
  postForm: FormGroup;
  users = Array<User>();
  postUsers = Array<User>();

  // Convenience getter voor de formulier velden
  get f() { return this.postForm.controls; }

  @ViewChild('searchInput') searchInput: ElementRef;
  @ViewChild('postName') postName: ElementRef;

  constructor(private formBuilder: FormBuilder,
              private accountService: AccountService,
              public dialogRef: MatDialogRef<PostsAddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Post
              ) {}

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      postName: '',
      street: [this.data.address.street, Validators.required],
      houseNumber: [this.data.address.houseNumber, Validators.required],
      appartementBus: [this.data.address.appartementBus],
      zip: [this.data.address.zip, Validators.required],
      country: [this.data.address.country, Validators.required],
    });

    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => this.users = users);
  }

  submit(postForm) {
    this.data.name = postForm.value.postName;
    this.data.users = this.postUsers;
    this.data.company = null;
    this.dialogRef.close(this.data);
  }

  close(): void {
    this.dialogRef.close();
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.postUsers.push(event.option.value);
    this.searchInput.nativeElement.value = '';
    this.postName.nativeElement.focus();
  }
}
