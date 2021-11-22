import { HttpWrapperService } from './services/http-wrapper.service';
import { PhonebookService } from './services/phonebook.service';
import { PhonebookModule } from './phonebook/phonebook.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule } from '@angular/common/http';

// Material
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTableModule } from '@angular/material/table';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    PhonebookModule,
    MatExpansionModule,
    MatTableModule,
  ],
  providers: [PhonebookService, HttpWrapperService],
  bootstrap: [AppComponent],
})
export class AppModule {}
