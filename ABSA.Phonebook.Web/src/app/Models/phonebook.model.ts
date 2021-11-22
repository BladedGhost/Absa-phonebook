import { EntryModel } from './entry.model';

export interface PhonebookModel {
  id: string;
  name: string;
  entries: Array<EntryModel>;
}
