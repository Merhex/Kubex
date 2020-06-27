import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService, AlertService } from 'src/app/_services';
import { first } from 'rxjs/operators';
import { User, Address, UserRegister } from 'src/app/_models';

import {Location, Appearance, GermanAddress} from '@angular-material-extensions/google-maps-autocomplete';
// import {} from '@types/googlemaps';
import PlaceResult = google.maps.places.PlaceResult;

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
  userName: string;
  user = new User();
  addressFormGroup: FormGroup;
  addressValue: Address = {
      country: 'Belgium',
      street: 'You street',
      houseNumber: 0,
      zip: '2200'
  };

  // Google Places attributes
  public appearance = Appearance;
  public zoom: number;
  public latitude: number;
  public longitude: number;
  public selectedAddress: PlaceResult;

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private accountService: AccountService,
      private alertService: AlertService
  ) {}

  // Convenience getter voor de formulier velden
  get f() { return this.form.controls; }

  ngOnInit() {
      this.userName = this.route.snapshot.params.username;
      this.isAddMode = !this.userName;

      this.zoom = 10;
      this.latitude = 52.520008;
      this.longitude = 13.404954;

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
          street: ['', Validators.required],
          houseNumber: ['', Validators.required],
          zip: ['', Validators.required],
          country: ['', Validators.required]
      });

      // In edit mode moeten de velden worden opgevuld met de data in DB
      if (!this.isAddMode) {
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
              });
      }
  }

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
      const userRegister = new UserRegister();
      const addressRegister = new Address();

      // We steken de form data in de nieuwe User.
      userRegister.firstName = this.f.firstName.value;
      userRegister.lastName = this.f.lastName.value;
      userRegister.userName = this.f.userName.value;
      userRegister.password = this.f.password.value;

      // Zelfde voor het nieuwe Address
      addressRegister.street = this.f.street.value;
      addressRegister.houseNumber = this.f.houseNumber.value;
      addressRegister.zip = this.f.zip.value;
      addressRegister.country = this.f.country.value;

      // We koppelen het Address aan de User
      userRegister.address = addressRegister;

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
      this.accountService.update(this.userName, this.form.value)
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

  // Google Maps
  onAutocompleteSelected(result: PlaceResult) {
    console.log('onAutocompleteSelected: ', result);
  }

  onLocationSelected(location: Location) {
    console.log('onLocationSelected: ', location);
    this.latitude = location.latitude;
    this.longitude = location.longitude;
  }

  onGermanAddressMapped($event: GermanAddress) {
    console.log('onGermanAddressMapped', $event);
  }

}
