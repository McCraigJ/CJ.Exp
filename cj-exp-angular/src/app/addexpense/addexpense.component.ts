import { OnInit, Component, OnDestroy } from '@angular/core';
import { ExpensesService } from '../_services/expenses.service';
import { Subscription } from 'rxjs';
import { ExpenseType } from '../_models/expense.models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormStatus } from '../_models/form.models';
import { currencyValidator } from '../_validators/currencyValidator';

@Component({ templateUrl: 'addexpense.component.html' })
export class AddExpenseComponent implements OnInit, OnDestroy {
    constructor(
        private formBuilder: FormBuilder,
        private expensesService: ExpensesService
    ) {
        this.formStatus = new FormStatus();
    }

    addExpenseForm: FormGroup;
    formStatus: FormStatus;
    expenseTypesSubscription: Subscription;
    expenseTypes: ExpenseType[];
    showNewExpenseType: boolean = false;

    // convenience getter for easy access to form fields
    get f() { return this.addExpenseForm.controls; }

    ngOnInit() {

        this.formStatus.loading = true;
        this.addExpenseForm = this.formBuilder.group({
            expenseType: ['', Validators.required],
            newExpenseType: [''],
            expenseDate: [new Date(), Validators.required],
            expenseValue: [0, [Validators.required, currencyValidator]]
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

        var newExpenseTypeControl = this.addExpenseForm.get('newExpenseType');

        this.addExpenseForm.get('expenseType').valueChanges
            .subscribe(expenseType => {
                this.showNewExpenseType = expenseType === '-1';
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

        // stop here if form is invalid
        if (this.addExpenseForm.invalid) {
            return;
        }

        this.formStatus.submitExecuting = true;

        // this.loading = true;
        // this.authenticationService.login(this.f.username.value, this.f.password.value)
        //     .pipe(first())
        //     .subscribe(
        //         data => {
        //             if (data === undefined) {
        //                 this.alertService.error('Invalid login details');
        //                 this.loading = false;
        //             } else {
        //                 this.router.navigate([this.returnUrl]);
        //             }
        //         },
        //         error => {
        //             this.alertService.error('Error communicating with server');
        //             this.loading = false;
        //         });
    }
}
