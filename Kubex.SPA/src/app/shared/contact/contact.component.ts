import { Component, Input } from '@angular/core';
import { ControlContainer, AbstractControl, FormGroup } from '@angular/forms';
import { ContactService } from 'src/app/_services/contact.service';
import { AlertService } from 'src/app/_services';
import { Contact } from 'src/app/_models/contact';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  @Input() companyId: number;
  @Input() userId: number;
  contacts: Contact[];

  constructor(
    public controlContainer: ControlContainer,
    private contactService: ContactService,
    private alertService: AlertService) { }

  addContact() {
    const contact = {
      type: this.getControls().type.value,
      value: this.getControls().value.value,
      companyId: this.companyId,
      userId: this.userId,
    };

    this.contactService.add(contact).subscribe(
      (data) => {
        this.alertService.success('Contact has been created.');

      },
      (error) => {
      }
    );
  }

  getControls() { const c = this.controlContainer.control as FormGroup; return c.controls; }
}
