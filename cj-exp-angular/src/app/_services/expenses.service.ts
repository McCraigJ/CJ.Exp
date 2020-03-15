import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { expenseType, expense } from '../_models/expense.models';
import { ApiResponse } from '../_models/apiResponse.models';

@Injectable({ providedIn: 'root' })
export class ExpensesService {

  private expenseTypesSubject: BehaviorSubject<expenseType[]>;

  constructor(
    private http: HttpClient
  ) {
    this.expenseTypesSubject = new BehaviorSubject<expenseType[]>([]);
  }

  getExpenseTypes(): Observable<expenseType[]> {
     return this.http.get<ApiResponse>(`${environment.apiUrl}expenses/GetexpenseTypes`)
      .pipe(map(apiResponse => {
          const expenseTypes: expenseType[] = apiResponse.data;
          this.expenseTypesSubject.next(expenseTypes);
          return expenseTypes;
      }));
  }

  addExpense(expense: expense): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${environment.apiUrl}expenses/Add`, expense)
    .pipe();
  }
}
