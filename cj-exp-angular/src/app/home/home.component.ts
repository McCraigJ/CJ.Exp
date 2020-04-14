import { Component, OnInit, OnDestroy} from '@angular/core';
import { Subscription } from 'rxjs';
import { ExpensesService } from '../_services/expenses.service';
import { first } from 'rxjs/operators';
import { FormStatus } from '../_models/form.models';
import { Router } from '@angular/router';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit, OnDestroy {

  loaded = false;
  todaysExpensesSubscription: Subscription;
  todaysExpensesTotal?:number;
  formStatus: FormStatus;
  hasExpensesToday: boolean;

  constructor(
    private expensesService: ExpensesService,
    private router: Router
  ) {
    this.formStatus = new FormStatus();
    this.hasExpensesToday = false;  
  }

  ngOnInit() {
    this.formStatus.loading = true;
    var todaysDate = new Date();
    this.todaysExpensesSubscription = this.expensesService.getExpenses({
      startDate: todaysDate,
      endDate: todaysDate,
      itemsPerPage: 20,
      pageNumber: 1
    }).pipe(first()).subscribe(
      data => {
        this.todaysExpensesTotal = data.dataSetTotal;
        this.hasExpensesToday = this.todaysExpensesTotal !== null;
        this.formStatus.loading = false;
      },
      error => {
        this.formStatus.loading = false;
        this.formStatus.loadError = true;
      }
    );
  }

  gotoAddExpense() {
    this.router.navigate(['/addexpense']);
  }

  ngOnDestroy() {
    this.todaysExpensesSubscription.unsubscribe();
  }

}
