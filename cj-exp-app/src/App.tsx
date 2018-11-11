import { AppBar, Grid, Toolbar, Typography } from '@material-ui/core';
import { ConnectedRouter } from 'connected-react-router';
import { History } from 'history';
import * as React from 'react';
import './App.css';
import Navigation from './components/Navigation';
import Routes from './Routes';

interface AppProps {
  history: History
}

export default class App extends React.Component<AppProps> {
  public render() {
    const { history } = this.props;
    return (
      <div className="App">
        <ConnectedRouter history={history}>
          <React.Fragment>
            <AppBar title="My App">
              <Toolbar>
                <Typography variant="title" color="inherit">Expenses</Typography>
              </Toolbar>
            </AppBar>
            <Grid container spacing={24}>
              <Grid item xs={12}>
                <Routes />
              </Grid>
            </Grid>
            <Navigation />
          </React.Fragment>
        </ConnectedRouter>
      </div>
    );
  }
}