import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService, AlertService } from 'src/app/_services';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class AddEditComponent implements OnInit {
  form: FormGroup;
  id: number;
  isAddMode: boolean;
  loading = false;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private accountService: AccountService,
      private alertService: AlertService
  ) {}

  ngOnInit() {
      this.id = this.route.snapshot.params['id'];
      this.isAddMode = !this.id;

      // Wanneer in edit-mode: geen ww vereist
      const passwordValidators = [Validators.minLength(6)];
      if (this.isAddMode) {
          passwordValidators.push(Validators.required);
      }

      this.form = this.formBuilder.group({
          firstName: ['', Validators.required],
          lastName: ['', Validators.required],
          username: ['', Validators.required],
          password: ['', passwordValidators]
      });

      if (!this.isAddMode) {
          this.accountService.getById(this.id)
              .pipe(first())
              .subscribe(x => {
                  this.f.firstName.setValue(x.firstName);
                  this.f.lastName.setValue(x.lastName);
                  this.f.username.setValue(x.username);
              });
      }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
      this.submitted = true;

      this.alertService.clear();

      if (this.form.invalid) {
          return;
      }

      this.loading = true;
      if (this.isAddMode) {
          this.createUser();
      } else {
          this.updateUser();
      }
  }

  private createUser() {
      this.accountService.register(this.form.value)
          .pipe(first())
          .subscribe(
              data => {
                  this.alertService.success('User added successfully', { keepAfterRouteChange: true });
                  this.router.navigate(['.', { relativeTo: this.route }]);
              },
              error => {
                  this.alertService.error(error);
                  this.loading = false;
              });
  }

  private updateUser() {
      this.accountService.update(this.id, this.form.value)
          .pipe(first())
          .subscribe(
              data => {
                  this.alertService.success('Update successful', { keepAfterRouteChange: true });
                  this.router.navigate(['..', { relativeTo: this.route }]);
              },
              error => {
                  this.alertService.error(error);
                  this.loading = false;
              });
  }
}
