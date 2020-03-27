import { User } from './user.models';
import { inherits } from 'util';
import { GridResponse } from './apiResponse.models';

export class ExpenseType {
  id: string;
  name: string;
}

export class AddExpense {
  expenseTypeId: string;
  newExpenseType: string;
  expenseDate: Date;
  expenseValue: number;
  // expenseUser: string;
}

export class ExpenseGrid extends GridResponse {
  gridRows: ExpenseGridRow[];
}

export class ExpenseGridRow {
  id: string;
  expenseType: ExpenseType;
  expenseDate: Date;
  expenseValue: number;
  user: User;
}

export class ExpensesFilter {
  startDate: Date;
  endDate: Date;
  itemsPerPage: number;
  pageNumber: number;
}
