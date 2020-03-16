import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ExpenseType, Expense } from '../_models/expense.models';
import { ApiResponse } from '../_models/apiResponse.models';

@Injectable({ providedIn: 'root' })
export class ExpensesService {

  private expenseTypesSubject: BehaviorSubject<ExpenseType[]>;

  constructor(
    private http: HttpClient
  ) {
    this.expenseTypesSubject = new BehaviorSubject<ExpenseType[]>([]);
  }

  getExpenseTypes(): Observable<ExpenseType[]> {
     return this.http.get<ApiResponse>(`${environment.apiUrl}expenses/GetexpenseTypes`)
      .pipe(map(apiResponse => {
          const expenseTypes: ExpenseType[] = apiResponse.data;
          this.expenseTypesSubject.next(expenseTypes);
          return expenseTypes;
      }));
  }

  addExpense(expense: Expense): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${environment.apiUrl}expenses/Add`, expense)
    .pipe();
  }
}
