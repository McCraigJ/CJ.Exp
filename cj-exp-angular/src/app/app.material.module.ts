import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';

// import {
//   MatIconModule,
//   MatBadgeModule,
//   MatFormFieldModule,
//   MatInputModule,
//   MatSelectModule,
//   MatRadioModule,
//   MatNativeDateModule,
//   MatChipsModule,
//   MatTooltipModule,
//   MatTableModule,
//   MatPaginatorModule,
//   MatCardModule
// } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatSidenavModule,
    MatListModule,
    MatGridListModule,
    MatIconModule
  ],
  exports: [
    MatButtonModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatSidenavModule,
    MatListModule,
    MatGridListModule,
    MatIconModule
  ],
  providers: [
    MatDatepickerModule
  ]
})
export class AngularMaterialModule { }
