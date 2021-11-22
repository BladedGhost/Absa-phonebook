import { EditPhonebookComponent } from './phonebook/edit-phonebook/edit-phonebook.component';
import { PhonebookComponent } from './phonebook/phonebook.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    component: PhonebookComponent,
    path: '',
  },
  {
    component: EditPhonebookComponent,
    path: 'edit/:id',
  },
  {
    component: EditPhonebookComponent,
    path: 'edit',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
