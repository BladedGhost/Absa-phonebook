import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPhonebookComponent } from './edit-phonebook.component';

describe('EditPhonebookComponent', () => {
  let component: EditPhonebookComponent;
  let fixture: ComponentFixture<EditPhonebookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPhonebookComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPhonebookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
