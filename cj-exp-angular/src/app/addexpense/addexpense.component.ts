import { OnInit, Component, OnDestroy } from '@angular/core';
import { ExpensesService } from '../_services/expenses.service';
import { Subscription } from 'rxjs';
import { ExpenseType } from '../_models/expense.models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormStatus } from '../_models/form.models';
import { currencyValidator } from '../_validators/currencyValidator';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AlertService } from '../_services/alert.service';

@Component({ templateUrl: 'addexpense.component.html' })
export class AddExpenseComponent implements OnInit, OnDestroy {
    constructor(
        private formBuilder: FormBuilder,
        private expensesService: ExpensesService,
        private router: Router,
        private alertService: AlertService
    ) {
        this.formStatus = new FormStatus();
    }

    addExpenseForm: FormGroup;
    formStatus: FormStatus;
    expenseTypesSubscription: Subscription;
    expenseTypes: ExpenseType[];
    showNewExpenseType = false;

    // convenience getter for easy access to form fields
    get f() { return this.addExpenseForm.controls; }

    ngOnInit() {

        this.formStatus.loading = true;
        this.addExpenseForm = this.formBuilder.group({
            expenseType: ['', Validators.required],
            newExpenseType: [''],
            expenseDate: [new Date(), Validators.required],
            expenseValue: ['', [Validators.required, currencyValidator]]
        });

        this.expenseTypesSubscription = this.expensesService.getExpenseTypes()
            .subscribe(expenseTypes => {
                const otherExpenseType: ExpenseType = { id: '-1', name: 'Other' };
                expenseTypes.push(otherExpenseType);
                this.expenseTypes = expenseTypes;
                this.formStatus.loading = false;
                this.formStatus.loadError = false;
            }, error => {
                this.formStatus.loading = false;
                this.formStatus.loadError = true;
            });

        const newExpenseTypeControl = this.addExpenseForm.get('newExpenseType');

        this.addExpenseForm.get('expenseType').valueChanges
            .subscribe(expType => {
                this.showNewExpenseType = expType === '-1';
                if (this.showNewExpenseType) {
                    newExpenseTypeControl.setValidators([Validators.required]);
                } else {
                    newExpenseTypeControl.setValidators(null);
                }

                newExpenseTypeControl.updateValueAndValidity();
            });
    }

    ngOnDestroy() {
        this.expenseTypesSubscription.unsubscribe();
    }

    onSubmit() {
        this.formStatus.submitted = true;

        if (this.addExpenseForm.invalid) {
            return;
        }

        this.formStatus.submitExecuting = true;

        this.expensesService.addExpense({
            expenseTypeId: this.f.expenseType.value,
            newExpenseType: this.f.newExpenseType.value,
            expenseDate: this.f.expenseDate.value,
            expenseValue: this.f.expenseValue.value
        }).pipe(first()).subscribe(
            data => {
                if (data.success) {
                    this.alertService.success('Expense added');
                    this.router.navigate(['/']);
                } else {
                    this.alertService.error(data.businessErrors[0].errorMessage);
                    this.formStatus.submitExecuting = false;
                }
            },
            error => {
                this.alertService.error('Error communicating with server');
                this.formStatus.submitExecuting = false;
            }
        );

    }
}
