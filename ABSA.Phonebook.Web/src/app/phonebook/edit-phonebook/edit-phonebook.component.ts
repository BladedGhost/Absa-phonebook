import { Subscription } from 'rxjs';
import { EntryModel } from './../../Models/entry.model';
import { PhonebookService } from './../../services/phonebook.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PhonebookModel } from 'src/app/Models/phonebook.model';
import { EntryService } from 'src/app/services/entry.service';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-edit-phonebook',
  templateUrl: './edit-phonebook.component.html',
  styleUrls: ['./edit-phonebook.component.scss'],
})
export class EditPhonebookComponent implements OnInit {
  title = '';
  recordID = '';
  phonebookData: PhonebookModel = { entries: [], id: '', name: '' };
  newentryIds: Array<string> = [];
  entries: Array<EntryModel> = [];
  constructor(
    activatedRoute: ActivatedRoute,
    private router: Router,
    private phonebookService: PhonebookService,
    private entryService: EntryService
  ) {
    activatedRoute.params.subscribe((x) => {
      this.recordID = x['id'];
      if (this.recordID) {
        phonebookService.GetPhonebookByID(this.recordID).subscribe((p) => {
          if (p.success) {
            this.phonebookData = p.data;
            this.entries = this.phonebookData.entries;
          } else alert(p.errorMessage);
        });
        this.title = 'Edit Phonebook';
      } else {
        this.title = 'New Phonebook';
        this.phonebookData.id = Guid.create().toString();
      }
    });
  }

  ngOnInit(): void {}

  save() {
    if (!this.recordID) {
      const sub = this.phonebookService
        .CreatePhonebook(this.phonebookData)
        .subscribe((x) => {
          if (!x.success) alert(x.errorMessage);
          sub.unsubscribe();
          this.router.navigate(['']);
        });
    } else {
      const sub = this.phonebookService
        .UpdatePhonebook(this.phonebookData)
        .subscribe((x) => {
          if (!x.success) alert(x.errorMessage);
          sub.unsubscribe();
          this.router.navigate(['']);
        });
    }
  }

  addNew() {
    const id = Guid.create().toString();
    this.phonebookData.entries.push({
      id,
      name: '',
      phoneNumber: '',
      phonebookID: this.phonebookData.id,
    });
    this.newentryIds.push(id);
    this.entries = JSON.parse(JSON.stringify(this.phonebookData.entries));
  }
  delete(id: string) {
    // remove entry which is not saved to the database yet
    if (this.newentryIds.find((x) => x === id)) {
      this.phonebookData.entries = this.phonebookData.entries.filter(
        (x) => x.id !== id
      );
      return;
    }
    const sub = this.entryService.DeleteEntry(id).subscribe((x) => {
      if (x.success)
        this.phonebookData.entries = this.phonebookData.entries.filter(
          (x) => x.id !== id
        );
      else alert(x.errorMessage);
      sub.unsubscribe();
    });
    this.entries = JSON.parse(JSON.stringify(this.phonebookData.entries));
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
    this.entries = JSON.parse(JSON.stringify(this.phonebookData.entries));
    this.entries = this.entries.filter((x) =>
      x.name.toLowerCase().includes(filterValue)
    );
  }
}
