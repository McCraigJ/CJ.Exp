export class expenseType {
  id: string;
  name: string;
}

export class expense {
  expenseTypeId: string;
  newExpenseType: string;
  expenseDate: Date;
  expenseValue: number;
}