import { PostCreate } from './../../_models/postCreate';
import { AccountService } from './../../_services/account.service';
import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { User } from 'src/app/_models';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatListOption } from '@angular/material/list';

@Component({
  selector: 'app-postsadddialog',
  templateUrl: './postsAddDialog.component.html',
  styleUrls: ['./postsAddDialog.component.css']
})
export class PostsAddDialogComponent implements OnInit {
  postForm: FormGroup;
  users = Array<User>();
  selectedUserNames = Array<string>();
  postUsers = Array<User>();

  // Convenience getter voor de formulier velden
  get f() { return this.postForm.controls; }

  @ViewChild('searchInput') searchInput: ElementRef;
  @ViewChild('postName') postName: ElementRef;

  constructor(private formBuilder: FormBuilder,
              private accountService: AccountService,
              public dialogRef: MatDialogRef<PostsAddDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: PostCreate
              ) {}

  ngOnInit() {
    this.postForm = this.formBuilder.group({
      postName: [this.data.name, Validators.required],
      street: [this.data.address.street, Validators.required],
      houseNumber: [this.data.address.houseNumber, Validators.required],
      appartementBus: [this.data.address.appartementBus],
      zip: [this.data.address.zip, Validators.required],
      country: [this.data.address.country, Validators.required],
    });

    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => this.users = users);
  }

  submit(postForm) {
    this.data.name = this.data.company.name + ' - ' + postForm.value.postName;
    this.data.userNames = this.selectedUserNames;
    this.data.company = null;
    this.dialogRef.close(this.data);
  }

  close(): void {
    this.dialogRef.close();
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    // Voeg toe aan geselecteerde gebruikers
    const selectedUser: User = event.option.value;
    this.postUsers.push(selectedUser);
    this.selectedUserNames.push(selectedUser.userName);

    // Haal de al geselecteerde gebruikers uit de mogelijkheden
    const index = this.users.indexOf(selectedUser);
    this.users.splice(index, 1);

    // Zet waarde van zoekveld op leeg en verwijder de fous zodat we deze in één klik opnieuw kunnen gebruiken
    this.searchInput.nativeElement.value = '';
    this.postName.nativeElement.focus();
  }

  deleteAgentFromPost(user: User) {
    // Haal agent uit de lijst van gekoppelde gebruikers
    const index = this.postUsers.indexOf(user);
    this.postUsers.splice(index, 1);

    // Haal username uit de lijst van gekoppelde gebruikers
    const indexUserName = this.selectedUserNames.indexOf(user.userName);
    this.selectedUserNames.splice(indexUserName, 1);

    // Zet gebruiker terug in selecteerbare gebruikers
    this.users.push(user);
  }
}
