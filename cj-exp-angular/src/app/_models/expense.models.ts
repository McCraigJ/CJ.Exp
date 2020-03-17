export class ExpenseType {
  id: string;
  name: string;
}

export class Expense {
  expenseTypeId: string;
  newExpenseType: string;
  expenseDate: Date;
  expenseValue: number;
  // expenseUser: string;
}

export class ExpensesFilter {
  startDate: Date;
  endDate: Date;
  itemsPerPage: number;
  pageNumber: number;
}
