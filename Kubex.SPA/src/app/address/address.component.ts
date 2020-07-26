import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ControlContainer } from '@angular/forms';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AddressComponent {

  constructor(public controlContainer: ControlContainer) { }

}
