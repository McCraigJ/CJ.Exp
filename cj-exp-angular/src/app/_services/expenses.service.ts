import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpService } from './http.service';
import { ExpenseType } from '../_models/expense.models';

@Injectable({ providedIn: 'root' })
export class ExpensesService {

  private expenseTypesSubject: BehaviorSubject<ExpenseType[]>;
  // public currentUser: Observable<ExpenseType[]>;

  constructor(
    // private http: HttpClient
    private httpService: HttpService
  ) {
    this.expenseTypesSubject = new BehaviorSubject<ExpenseType[]>([]);
  }

  getExpenseTypes(): Observable<ExpenseType[]> {
    // return this.http.get<ExpenseType[]>(`${environment.apiUrl}expenses/GetExpenseTypes`)
    return this.httpService.getRequest('expenses/GetExpenseTypes', {})
      .pipe(map(apiResponse => {
          const expenseTypes: ExpenseType[] = apiResponse.data;
          this.expenseTypesSubject.next(expenseTypes);
          return expenseTypes;
      }));
  }
}
