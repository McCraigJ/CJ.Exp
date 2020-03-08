import { OnInit, Component } from '@angular/core';

@Component({templateUrl: 'addexpense.component.html'})
export class AddExpenseComponent implements OnInit {
    constructor() {}

    loading:Boolean = false;
    submitted:Boolean = false;

    ngOnInit() {
        

    }
}