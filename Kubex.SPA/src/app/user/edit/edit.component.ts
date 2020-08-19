import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService, AlertService } from 'src/app/_services';
import { first } from 'rxjs/operators';
import { User, Address, UserRegister } from 'src/app/_models';
import { CompanyService } from 'src/app/_services/company.service';

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
  hidePassword = true;
  hideCurrentPassword = true;
  userName: string;
  user = new User();

  fileData: File = null;
  previewUrl: any = null;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService,
    private companyService: CompanyService
  ) {}

  // Convenience getter voor de formulier velden
  get f() { return this.form.controls; }

  ngOnInit() {
      this.userName = this.route.snapshot.params.username;
      this.isAddMode = !this.userName;

      // Wachtwoord moet minstens 6 karakters hebben
      const passwordValidators = [Validators.minLength(6)];

      // Alleen wanneer we registreren is een wachtwoord vereist
      if (this.isAddMode) {
          passwordValidators.push(Validators.required);
      }

      this.form = this.formBuilder.group({
          firstName: ['', Validators.required],
          lastName: ['', Validators.required],
          userName: ['', Validators.required],
          password: ['', passwordValidators],

          // Address
          street: ['', Validators.required],
          houseNumber: ['', Validators.required],
          appartementBus: [''],
          zip: ['', Validators.required],
          country: ['', Validators.required]
      });

      // In edit mode moeten de velden worden opgevuld met de data in DB
      if (!this.isAddMode) {
          this.form.addControl('currentPassword', new FormControl('', passwordValidators));

          this.accountService.getByUserName(this.userName)
              .pipe(first())
              .subscribe(user => {
                  // Opvullen van de velden dmv de convenience getter
                  this.f.firstName.setValue(user.firstName);
                  this.f.lastName.setValue(user.lastName);
                  this.f.userName.setValue(user.userName);
                  this.f.street.setValue(user.address.street);
                  this.f.houseNumber.setValue(user.address.houseNumber);
                  this.f.zip.setValue(user.address.zip);
                  this.f.country.setValue(user.address.country);
                  this.f.appartementBus.setValue(user.address.appartementBus);
                  this.previewUrl = user.photoUrl;
              });
      }
  }

  async onSubmit() {
      this.submitted = true;
      this.alertService.clear();
      this.loading = true;

      if (this.fileLoaded()) {
        await this.uploadFile();
      }

      if (this.isAddMode) {
          this.createUser();
      } else {
          this.updateUser();
      }
  }

  fileLoaded() { return this.fileData && this.fileData.size > 0; }

  private createUser() {
    const address = this.getAddressFromForm();
    const userRegister = this.getUserRegisterFromForm();

    userRegister.address = address;

        // We sturen onze User naar de accountService voor registratie
    this.accountService.register(userRegister)
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
    const address = this.getAddressFromForm();
    const userRegister = this.getUserRegisterFromForm();

    userRegister.address = address;
    userRegister.photoUrl = this.previewUrl;

    this.accountService.update(this.userName, userRegister)
        .pipe(first())
        .subscribe(
            data => {
                this.alertService.success('User succesfully updated!', { keepAfterRouteChange: true });
                this.loading = false;
            },
            error => {
                this.alertService.error(error);
                this.loading = false;
            });
  }

  private getAddressFromForm(): Address {
      const address = new Address();

      address.street = this.f.street.value;
      address.houseNumber = this.f.houseNumber.value;
      address.zip = this.f.zip.value;
      address.country = this.f.country.value;
      address.appartementBus = this.f.appartementBus.value;

      return address;
  }

  private getUserRegisterFromForm(): UserRegister {
    const userRegister = new UserRegister();

    userRegister.firstName = this.f.firstName.value;
    userRegister.lastName = this.f.lastName.value;
    userRegister.userName = this.f.userName.value;
    userRegister.password = this.f.password.value;

    if (!this.isAddMode) {
        userRegister.currentPassword = this.f.currentPassword.value;
    }

    return userRegister;
  }

  public fileProgress(fileInput: any) {
    this.fileData = fileInput.target.files[0] as File;
    this.preview();
  }

  private preview() {
    const filetype = this.fileData.type;

    if (filetype.match(/image\/*/) == null) {
      return;
    }

    const reader = new FileReader();
    reader.readAsDataURL(this.fileData);
    reader.onload = (event) => {
      this.previewUrl = reader.result;
    };
  }

  public async uploadFile(): Promise<void> {
    const formData = new FormData();
    formData.append('file', this.fileData);

    return new Promise<void>((resolve, reject) => {
      this.companyService.uploadFile(formData).subscribe(
        (res) => {
          console.log(res);

          this.previewUrl = res.path;
          this.alertService.success('The image has been succesfully uploaded!');
          this.loading = false;
          this.form.markAsUntouched();
          this.fileData = null;

          resolve();
        },
        (err) => {
          this.alertService.error(err);
          this.loading = false;

          reject();
        });
    });
  }
}
