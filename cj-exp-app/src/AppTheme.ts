import purple from '@material-ui/core/colors/purple';
import { createMuiTheme, createStyles, Theme } from '@material-ui/core/styles';
import orange from '../node_modules/@material-ui/core/colors/orange';


export const installTheme = createMuiTheme({
  palette: {
    primary: purple,
    secondary: orange
  },
});

export const styles = (theme: Theme) => createStyles({
  fullWidth: { display: "block !important;"},
  standardWidth: { marginRight: "1em !important;" },
});

export const buttonStyles = (theme: Theme) => createStyles({
  button: {
    marginBottom: "0.5em !important",
    marginRight: "0.5em !important",
    marginTop: "0.5em !important"
  }
});