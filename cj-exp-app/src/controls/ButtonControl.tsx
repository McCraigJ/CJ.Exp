import { Button, createStyles, Theme, withStyles, WithStyles } from "@material-ui/core";
import { ButtonProps } from "@material-ui/core/Button";
import classnames from "classnames";
import * as React from "react";
import { styles } from "../AppTheme";

// Join a local style with a higher level style
const allStyles  = (theme: Theme) => createStyles({
  ...styles(theme)
});

const ButtonControl: React.SFC<ButtonProps & WithStyles<typeof allStyles>> = (props) => (    
  <Button className={classnames(props.className)} component={props.component} variant={props.variant ||  "contained"} 
  color={props.color} disabled={props.disabled} onClick={props.onClick}>{props.children}</Button>
);

export default withStyles(allStyles)(ButtonControl);