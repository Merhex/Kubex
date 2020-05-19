import { Component, OnInit } from '@angular/core';
import { Address, Post } from 'src/app/_models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PostService } from 'src/app/_services/post.service';
import { AlertService } from 'src/app/_services';

@Component({
  selector: 'app-post-create',
  templateUrl: './post-create.component.html',
  styleUrls: ['./post-create.component.css']
})
export class PostCreateComponent implements OnInit {
  form: FormGroup;
  loading = false;

  constructor(private postService: PostService,
              private formBuilder: FormBuilder,
              private alertService: AlertService) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      street: ['', Validators.required],
      houseNumber: [''],
      appartementBus: [''],
      zip: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  create() {
    this.loading = true;

    const post = new Post();
    post.address = this.getAddressFromForm();
    post.name = this.form.controls.name.value;

    this.postService.create(post).subscribe(
      data => {
        this.alertService.success('Post added successfully');
        this.loading = false;
    },
      error => {
        this.alertService.error(error);
        this.loading = false;
    });
  }

  private getAddressFromForm(): Address {
    const address = new Address();

    address.street = this.form.controls.street.value;
    address.houseNumber = this.form.controls.houseNumber.value;
    address.zip = this.form.controls.zip.value;
    address.country = this.form.controls.country.value;
    address.appartementBus = this.form.controls.appartementBus.value;

    return address;
}
}
