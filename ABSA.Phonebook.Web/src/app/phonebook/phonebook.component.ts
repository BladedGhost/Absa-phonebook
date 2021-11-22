import { PhonebookService } from './../services/phonebook.service';
import { Component, OnInit } from '@angular/core';
import { EntryModel } from '../Models/entry.model';
import { PhonebookModel } from '../Models/phonebook.model';
import { Subscription } from 'rxjs';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ResultModel } from '../Models/result.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class PhonebookComponent implements OnInit {
  phonebookSrc: MatTableDataSource<PhonebookModel> =
    new MatTableDataSource<PhonebookModel>();
  phonebookColumns = ['name'];
  expandedElement: EntryModel | null = null;
  phonebookSub: Subscription = new Subscription();

  constructor(
    private phonebookService: PhonebookService,
    public dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.phonebookSub = this.phonebookService.GetPhonebooks().subscribe((x) => {
      if (x.success) {
        this.phonebookSrc = new MatTableDataSource(x.data);
      } else {
        alert(x.errorMessage);
      }
    });
  }

  applyFilter = (event: Event) => {
    const filterValue = (event.target as HTMLInputElement).value;
    this.phonebookSrc.filter = filterValue.trim().toLowerCase();
  };

  createRecord() {
    this.router.navigate(['edit']);
  }

  editRecorord(data: any) {
    this.router.navigate(['edit', data.id], { state: data });
  }

  toTitleCase(value: string) {
    const rep = (txt: string) => {
      return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    };
    return value.replace(/\w\S*/g, rep);
  }
}
