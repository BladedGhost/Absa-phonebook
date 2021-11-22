import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EntryModel } from '../Models/entry.model';
import { HttpWrapperService } from './http-wrapper.service';

@Injectable({
  providedIn: 'root',
})
export class EntryService {
  constructor(private httpPhone: HttpWrapperService<EntryModel>) {}

  public GetEntrys() {
    return this.httpPhone.getByType<Array<EntryModel>>(environment.paths.Entry);
  }

  public GetEntryByID(id: string) {
    return this.httpPhone.get(`${environment.paths.Entry}/${id}`);
  }

  public CreateEntry(entry: EntryModel) {
    return this.httpPhone.post(environment.paths.Entry, entry);
  }

  public UpdateEntry(entry: EntryModel) {
    return this.httpPhone.put(environment.paths.Entry, entry);
  }

  public DeleteEntry(id: string) {
    return this.httpPhone.delete(environment.paths.Entry, id);
  }
}
