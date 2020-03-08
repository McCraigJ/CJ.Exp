import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from '../app-routing.module';
import { AppComponent } from '../app/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './app.material.module';
import { HomeComponent } from '../home';
import { LoginComponent } from '../login';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import {  PageHeaderComponent } from '../_components';
import { AddExpenseComponent } from '../addexpense';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    PageHeaderComponent,
    AddExpenseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
