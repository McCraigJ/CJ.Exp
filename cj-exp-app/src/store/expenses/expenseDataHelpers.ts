import { Expense } from './types';

export function FilterExpenses(data: Expense[], filterId: string):Expense {
  const filtered = data.filter(item => item.id === filterId);
  if (filtered.length > 0) {
    return filtered[0];
  }
  return getBlankExpense();
}

export function getBlankExpense():Expense {
  return {
    id: "",
    expenseTypeId: "",
    amount: 0,
    description: ""
  };
}