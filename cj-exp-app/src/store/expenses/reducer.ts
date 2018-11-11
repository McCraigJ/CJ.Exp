// const initialState = [];
import { Reducer } from 'redux';
import { Expense, ExpenseActionTypes, ExpensesState } from './types'


const initialState: ExpensesState = {
  expenses: [],
  isSyncing: false ,
  expenseTypes: [] 
}

const reducer: Reducer<ExpensesState> = (state = initialState, action) => {
  switch (action.type) {
    case ExpenseActionTypes.ADD: 
      return { ...state, data: [...state.expenses, action.payload ]};

    case ExpenseActionTypes.EDIT: 
      return { ...state, data: updateData(state.expenses, action.payload)};      
   
    default: 
      return state;    
  }
}

function updateData(currentExpenses:Expense[], updatedExpense:Expense):Expense[] {
  return currentExpenses.map(exp => {
    if (exp.id === updatedExpense.id) {
      return Object.assign({}, exp, updatedExpense);
    }
    return exp;
  });
}

export { reducer as expensesReducer }
