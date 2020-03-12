import { OnInit, Component, OnDestroy } from '@angular/core';
import { ExpensesService } from '../_services/expenses.service';
import { Subscription } from 'rxjs';
import { ExpenseType } from '../_models/expense.models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormStatus } from '../_models/form.models';

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

    // convenience getter for easy access to form fields
    get f() { return this.addExpenseForm.controls; }

    ngOnInit() {

        this.formStatus.loading = true;
        this.addExpenseForm = this.formBuilder.group({
            expenseType: ['', Validators.required]
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

        this.formStatus.submitting = true;

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
