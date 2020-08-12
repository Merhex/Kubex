import { Component } from '@angular/core';
import { ControlContainer } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent {

  constructor(public controlContainer: ControlContainer) { }

  addContact() {
    console.log('Adding contact.');
  }
}
