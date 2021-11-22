import { environment } from './../../environments/environment';
import { HttpWrapperService } from './http-wrapper.service';
import { Injectable, OnDestroy } from '@angular/core';
import { PhonebookModel } from '../Models/phonebook.model';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PhonebookService {
  constructor(private httpPhone: HttpWrapperService<PhonebookModel>) {}

  public GetPhonebooks() {
    return this.httpPhone.getByType<Array<PhonebookModel>>(
      environment.paths.phonebook
    );
  }

  public GetPhonebookByID(id: string) {
    return this.httpPhone.get(`${environment.paths.phonebook}/${id}`);
  }

  public GetPhonebooksByName(name: string) {
    return this.httpPhone.getByType<Array<PhonebookModel>>(
      `${environment.paths.phonebook}/search/${name}`
    );
  }

  public CreatePhonebook(phonebook: PhonebookModel) {
    return this.httpPhone.post(environment.paths.phonebook, phonebook);
  }

  public UpdatePhonebook(phonebook: PhonebookModel) {
    return this.httpPhone.put(environment.paths.phonebook, phonebook);
  }

  public DeletePhonebook(id: string) {
    return this.httpPhone.delete(environment.paths.phonebook, id);
  }
}
