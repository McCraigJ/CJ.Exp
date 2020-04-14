import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormStatus } from '../_models/form.models';
import { Subscription } from 'rxjs';
import { ExpensesService } from '../_services/expenses.service';
import { first } from 'rxjs/operators';
import { ExpenseGrid } from '../_models/expense.models';
import { Router } from '@angular/router';

@Component({ templateUrl: 'getexpenses.component.html' })
export class GetExpensesComponent implements OnInit, OnDestroy {

  getExpensesFilterForm: FormGroup;
  formStatus: FormStatus;
  expensesSubscription: Subscription;
  expenses: ExpenseGrid;
  currentPageNumber = 1;  
  showTodayOnly = false;

  constructor(
    private formBuilder: FormBuilder,
    private expensesService: ExpensesService,
    private router: Router
  ) {
    this.formStatus = new FormStatus();
    var routerState = this.router.getCurrentNavigation().extras.state;
    if (routerState !== undefined) {
      this.showTodayOnly = routerState.showToday;
    }    
  }

  get f() { return this.getExpensesFilterForm.controls; }

  ngOnInit() {
    
    const currentDate: Date = new Date();
    const defaultFromDate = new Date();
    
    if (!this.showTodayOnly) {
      defaultFromDate.setDate(currentDate.getDate() - 7);
    }

    this.formStatus.loading = true;
    this.getExpensesFilterForm = this.formBuilder.group({
      dateFrom: [defaultFromDate],
      dateTo: [currentDate],
    });
    this.formStatus.loading = false;

    if (this.showTodayOnly) {
      this.formStatus.submitted = true;
      this.getExpenses();
    }
  }

  ngOnDestroy() {
    if (this.expensesSubscription != null) {
      this.expensesSubscription.unsubscribe();
    }    
  }

  onSubmit() {
    this.formStatus.submitted = true;

    if (this.getExpensesFilterForm.invalid) {
      return;
    }

    this.getExpenses();
  }

  private getExpenses() {
    

    this.formStatus.submitExecuting = true;
    this.formStatus.updateFilter = false;

    this.expensesSubscription = this.expensesService.getExpenses({
      startDate: this.f.dateFrom.value,
      endDate: this.f.dateTo.value,
      itemsPerPage: 20,
      pageNumber: this.currentPageNumber
    }).pipe(first()).subscribe(
      data => {
        this.formStatus.submitExecuting = false;
        this.expenses = data;
      },
      error => {
        this.formStatus.loading = false;
        this.formStatus.loadError = true;
      }
    );
  }

  updateFilter() {
    this.formStatus.updateFilter = true;
  }
}
