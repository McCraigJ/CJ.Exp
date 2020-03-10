import { OnInit, Component, OnDestroy } from '@angular/core';
import { ExpensesService } from '../_services/expenses.service';
import { Subscription } from 'rxjs';
import { ExpenseType } from '../_models/user';

@Component({templateUrl: 'addexpense.component.html'})
export class AddExpenseComponent implements OnInit, OnDestroy {
    constructor(
        private expensesService: ExpensesService
    ) {}

    loading = false;
    submitted = false;
    private expenseTypesSubscription: Subscription;
    expenseTypes: ExpenseType[];

    ngOnInit() {
        this.expenseTypesSubscription = this.expensesService.getExpenseTypes()
            .subscribe(expenseTypes => {
                this.expenseTypes = expenseTypes;
            });
    }

    ngOnDestroy() {
        this.expenseTypesSubscription.unsubscribe();
    }
}
