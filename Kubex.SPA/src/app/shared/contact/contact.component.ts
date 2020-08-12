import { Component } from '@angular/core';
import { ControlContainer, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {

  constructor(public controlContainer: ControlContainer) { }

  addContact() {
    console.log(this.controlContainer.name);
  }
}
