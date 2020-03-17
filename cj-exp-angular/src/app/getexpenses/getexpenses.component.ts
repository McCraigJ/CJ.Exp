import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormStatus } from '../_models/form.models';
import { Subscription } from 'rxjs';
import { Expense, ExpensesFilter } from '../_models/expense.models';
import { ExpensesService } from '../_services/expenses.service';

@Component({ templateUrl: 'getexpenses.component.html' })
export class GetExpensesComponent implements OnInit {

  getExpensesFilterForm: FormGroup;
  formStatus: FormStatus;
  expensesSubscription: Subscription;
  expenses: Expense[];
  currentPageNumber = 1;

  constructor(
    private formBuilder: FormBuilder,
    private expensesService: ExpensesService
  ) {
    this.formStatus = new FormStatus();
  }

  get f() { return this.getExpensesFilterForm.controls; }

  ngOnInit() {
    const currentDate: Date = new Date();
    const defaultFromDate = new Date();
    defaultFromDate.setDate(currentDate.getDate() - 7);

    this.formStatus.loading = true;
    this.getExpensesFilterForm = this.formBuilder.group({
      dateFrom: [defaultFromDate],
      dateTo: [currentDate],
    });
    this.formStatus.loading = false;
  }

  onSubmit() {
    this.formStatus.submitted = true;

    if (this.getExpensesFilterForm.invalid) {
      return;
    }

    this.formStatus.submitExecuting = true;

    this.expensesSubscription = this.expensesService.getExpenses({
      startDate: this.f.dateFrom.value,
      endDate: this.f.dateTo.value,
      itemsPerPage: 20,
      pageNumber: this.currentPageNumber
    }).pipe().subscribe(
      data => {
        this.expenses = data;
      },
      error => {
        this.formStatus.loading = false;
        this.formStatus.loadError = true;
      }
    );
    // Do form submit
    // this.formStatus.submitExecuting = false;
  }
}
