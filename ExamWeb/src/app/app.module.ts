import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CountryListComponent } from './components/country-list/country-list.component';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { CountryCreateComponent } from './components/country-create/country-create.component';
import { CountryUpdateComponent } from './components/country-update/country-update.component';
import { EmployeeListComponent } from './components/employee/employee-list/employee-list.component';
import { EmployeeCreateComponent } from './components/employee/employee-create/employee-create.component';
import { EmployeeUpdateComponent } from './components/employee/employee-update/employee-update.component';

@NgModule({
  declarations: [
    AppComponent,
    CountryListComponent,
    CountryCreateComponent,
    CountryUpdateComponent,
    EmployeeListComponent,
    EmployeeCreateComponent,
    EmployeeUpdateComponent
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule
  ],

  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }