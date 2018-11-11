import CssBaseline from '@material-ui/core/CssBaseline';
import configureStore from './store/configureStore';

import { createHashHistory } from 'history'

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from "react-redux";
// import { ConnectedRouter } from 'connected-react-router';


import App from './App';

import './index.css';

import registerServiceWorker from './registerServiceWorker';

import 'typeface-roboto';

const history = createHashHistory();

const store = configureStore(history);

ReactDOM.render(
  <div>
    <CssBaseline />
    <Provider store={store}>    
    <App history={history}/>
    </Provider>
  </div>,
  document.getElementById('root') as HTMLElement
);
registerServiceWorker();
