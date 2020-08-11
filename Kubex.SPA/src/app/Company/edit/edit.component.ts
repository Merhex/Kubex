import { UploadResponse } from './../../_models/uploadResponse';
import { first } from 'rxjs/operators';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from 'src/app/_services/company.service';
import { AlertService } from 'src/app/_services';
import { Address } from 'src/app/_models';
import { CompanyRegister } from 'src/app/_models/companyRegister';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  companyForm: FormGroup;
  submitted = false;
  loading = false;
  isAddMode: boolean;
  id: number;
  logoUrl: string;
  fileData: File = null;
  previewUrl: any = null;
  response: UploadResponse;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private companyService: CompanyService,
    private alertService: AlertService,
    private ref: ChangeDetectorRef
  ) { }

  // Convenience getter voor de formulier velden
  get f() { return this.companyForm.controls; }

  ngOnInit() {
    this.id = this.route.snapshot.params.id;
    this.isAddMode = !this.id;

    this.companyForm = this.formBuilder.group({
      name: ['', Validators.required],
      customerNumber: [''],

      // Address
      street: ['', Validators.required],
      houseNumber: [''],
      appartementBus: [''],
      zip: ['', Validators.required],
      country: ['', Validators.required],

      // Contact
      type: [''],
      value: ['']
    });

    // In edit mode moeten de velden worden opgevuld
    if (!this.isAddMode) {
      this.companyService.getCompanyById(this.id)
          .pipe(first())
          .subscribe(user => {
              // Opvullen van de velden dmv de convenience getter
              this.f.name.setValue(user.name);
              this.f.customerNumber.setValue(user.customerNumber);
              this.f.street.setValue(user.address.street);
              this.f.houseNumber.setValue(user.address.houseNumber);
              this.f.zip.setValue(user.address.zip);
              this.f.country.setValue(user.address.country);
              this.f.appartementBus.setValue(user.address.appartementBus);
          });
    }

    this.ref.detectChanges();
  }

  async onSubmit() {
    this.submitted = true;
    this.loading = true;
    this.alertService.clear();

    if (this.isAddMode) {
      this.createCompany();
    } else {
      this.updateCompany();
    }
  }

  private async createCompany() {
    const address = this.getAddressFromForm();
    const companyToRegister = this.getCompanyRegisterFromForm();

    companyToRegister.logoUrl = this.response.dbPath;
    companyToRegister.address = address;

    if (this.fileData.size > 0) {
      await this.uploadFile();
    }

    this.companyService.register(companyToRegister)
      .pipe(first())
      .subscribe(
        (data) => {
          console.log(data);
          this.alertService.success('Company successfully registered',  { keepAfterRouteChange: true });
          this.router.navigate(['.', { relativeTo: this.route }]);
        },
        (err) => {
            this.alertService.error(err);
            this.loading = false;
        });
  }

  private async updateCompany() {
    if (this.fileData.size > 0) {
      await this.uploadFile();
    }
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

  private getCompanyRegisterFromForm(): CompanyRegister {
    const companyToRegister = new CompanyRegister();

    companyToRegister.name = this.f.name.value;
    companyToRegister.customerNumber = this.f.customerNumber.value;

    return companyToRegister;
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

  public async uploadFile() {
    const formData = new FormData();
    formData.append('file', this.fileData);
    this.companyService.uploadFile(formData).subscribe(
      (res) => {
        this.response = res;
        this.alertService.success('The image has been succesfully uploaded!');
        this.loading = false;
      },
      (err) => {
        this.alertService.error(err);
        this.loading = false;
      });

      // console.log(this.response.dbPath);
  }
}
