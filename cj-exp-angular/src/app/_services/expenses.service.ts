import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ExpenseType } from '../_models/user';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ExpensesService {

  private expenseTypesSubject: BehaviorSubject<ExpenseType[]>;
  // public currentUser: Observable<ExpenseType[]>;

  constructor(
    private http: HttpClient
  ) {
    this.expenseTypesSubject = new BehaviorSubject<ExpenseType[]>([]);
  }

  getExpenseTypes() {
    return this.http.get<ExpenseType[]>(`${environment.apiUrl}expenses/GetExpenseTypes`)
      .pipe(map(expenseTypes => {
          // const expenseTypes: ExpenseType[] = apiResponse;
          this.expenseTypesSubject.next(expenseTypes);
          return expenseTypes;
      }));
  }
}
