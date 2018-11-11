import { action } from 'typesafe-actions';
import { Expense, ExpenseActionTypes } from './types';

export const addExpense = (data: Expense) => action(ExpenseActionTypes.ADD, data);
export const editExpense = (data: Expense) => action(ExpenseActionTypes.EDIT, data);