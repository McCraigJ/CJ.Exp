import { connectRouter, routerMiddleware } from 'connected-react-router'
import { History } from 'history'
import { applyMiddleware, createStore, Store } from 'redux'
import { ApplicationState, rootReducer } from './index'

export default function configureStore(
  history: History
): Store<ApplicationState> {  

  const initialState: ApplicationState = {
    expenseState: {
      expenses: [],
      isSyncing: false,
      expenseTypes: [{
        id: "1",
        name: "Groceries"
      },
      {
        id: "2",
        name: "Petrol"
      }]
    }
  };

  // We'll create our store with the combined reducers/sagas, and the initial Redux state that
  // we'll be passing from our entry point.
  const store = createStore(
    connectRouter(history)(rootReducer),    
    initialState,
    applyMiddleware(routerMiddleware(history))
  );

  // Don't forget to run the root saga, and return the store object.
  return store;
}
