import * as React from 'react'
import { Route, Switch } from 'react-router-dom'
import Home from './pages/Home';
import Settings from './pages/Settings';

const Routes: React.SFC = () => (
  <div>
    <Switch>
      <Route exact path="/" component={Home} />            
      <Route exact path="/Settings" component={Settings} />            
    </Switch>
  </div>
);

export default Routes;
