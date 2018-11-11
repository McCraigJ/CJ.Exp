import BottomNavigation from '@material-ui/core/BottomNavigation';
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction';
import AddIcon from '@material-ui/icons/Add';
import RestoreIcon from '@material-ui/icons/Restore';
import SettingsIcon from '@material-ui/icons/Settings';
import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router-dom';
// import { Link } from 'react-router-dom';
import './Navigation.css';

enum NavigationPages {
  Home = 0,
  Recent = 1,
  Settings = 2
}

interface NavigationState {
  currentPage: NavigationPages
}

class Navigation extends React.Component<RouteComponentProps<{}>, NavigationState> {

  constructor(props:any) {
    super(props);

    this.state = {
      currentPage: NavigationPages.Home
    };
    this.handleChange = this.handleChange.bind(this);
  }

  public render() {
    return (    
      <nav>
        <BottomNavigation
          value={this.state.currentPage}
          onChange={this.handleChange}
          showLabels
        // className={classes.root}
        >        
          <BottomNavigationAction label="New" icon={<AddIcon />} />
          <BottomNavigationAction label="Recent" icon={<RestoreIcon />} />
          <BottomNavigationAction label="Settings" icon={<SettingsIcon />} />
        </BottomNavigation>
      </nav>
    );
  }

  private handleChange(event:any, value:NavigationPages) {  
    this.setState({ currentPage: value });
    switch (value) {
      case NavigationPages.Home:
      this.props.history.push('/');
      break;
      case NavigationPages.Recent:
      this.props.history.push('/Recent');
      break;
      case NavigationPages.Settings:
      this.props.history.push('/Settings');
      break;
    }
    

  };
}

export default withRouter(Navigation);