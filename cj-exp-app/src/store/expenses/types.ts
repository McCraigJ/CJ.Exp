export interface Expense {
  id: string,
  expenseTypeId: string,  
  amount: number,
  description: string
}

export interface ExpenseType {
  id: string,
  name: string
}

export interface ExpensesState {
  expenses: Expense[],
  expenseTypes: ExpenseType[],
  isSyncing: boolean
}

export const enum ExpenseActionTypes {  
  ADD = '@@expense/ADD',
  EDIT = '@@expense/EDIT'
}