import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer, AbstractControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ContactService } from 'src/app/_services/contact.service';
import { AlertService } from 'src/app/_services';
import { Contact } from 'src/app/_models/contact';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  @Input() companyId: number;
  @Input() username: string;
  contacts: Contact[];
  newContactForm: FormGroup;
  addContactMode = false;
  loading = false;

  constructor(
    public controlContainer: ControlContainer,
    private contactService: ContactService,
    private alertService: AlertService,
    private formbuilder: FormBuilder) { }

  ngOnInit() {
    if (this.companyId) {
      this.contactService.getContactsForCompany(this.companyId).subscribe(
        (data) => {
          this.contacts = data;

          this.contacts.forEach(contact => {
            this.getControls().value.setValue(contact.value);
            this.getControls().type.setValue(contact.type);
          });
        },
        (err) => {
          this.alertService.error('Could not retrieve contacts for this company');
        });
    }
    if (this.username) {
      this.contactService.getContactsForUser(this.username).subscribe(
        (data) => {
          this.contacts = data;
        },
        (err) => {
          this.alertService.error('Could not retrieve contacts for this company');
        });
    }

    this.newContactForm = this.formbuilder.group({
      type: ['', Validators.required],
      value: ['', Validators.required]
    });
  }

  addContact() {
    this.loading = true;

    const contact = {
      id: undefined,
      type: this.newContactForm.controls.type.value,
      value: this.newContactForm.controls.value.value,
      companyId: this.companyId,
      username: this.username,
    };

    this.contactService.add(contact).subscribe(
      (data) => {
        this.alertService.success('Contact has been created.');
        this.loading = false;
        this.addContactMode = false;

        this.contacts.push(data);
      },
      (error) => {
        this.alertService.error(error);
        this.loading = false;
        this.addContactMode = false;
      }
    );
  }

  updateContact(id: number) {
    const contact = {
      id,
      type: this.getControls().type.value,
      value: this.getControls().value.value,
      companyId: this.companyId,
      username: this.username,
    };

    this.contactService.update(contact).subscribe(
      (data: Contact) => {
        let c = this.contacts.find(x => x.id === id);
        c = data;
        this.alertService.success('Succesfully updated contact.');
      }
    );
  }

  deleteContact(id: number) {
    this.contactService.delete(id).subscribe(
      (data) => {
        const contactId = this.contacts.findIndex(x => x.id === id);
        this.contacts.splice(contactId, 1);
        this.alertService.success('Succesfully removed contact.');
      },
      (err) => {
        this.alertService.error(err);
      });
  }

  getControls() { const c = this.controlContainer.control as FormGroup; return c.controls; }
}
