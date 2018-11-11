import { createStyles, FormControl, TextField, Theme, withStyles, WithStyles } from '@material-ui/core';
import * as React from 'react';
import { styles } from '../AppTheme';
import { FormFieldValidationResult } from '../components/form/types';
import { ValidationMessage } from '../components/form/ValidationMessage';

interface TextFieldControlType {
  name: string,
  label: string,
  value: string,
  required?: boolean,
  onChange: (event: any) => void,
  onBlur?: (event: any) => void,
  onKeyPress?: (event: any) => void,
  isFormError?: boolean,
  fieldMessages: FormFieldValidationResult[],
  password?: boolean,
  fullWidth?: boolean
  placeholder?: string
}

// Join a local style with a higher level style
const allStyles  = (theme: Theme) => createStyles({
  ...styles(theme)
});

const TextFieldControl: React.SFC<TextFieldControlType & WithStyles<typeof allStyles>> = (props) => (
  
  <FormControl className={props.fullWidth ? props.classes.fullWidth : props.classes.standardWidth}>
  
    <TextField id={props.name} name={props.name} label={props.label} value={props.value} required={props.required} margin="normal"
      onChange={props.onChange} onBlur={props.onBlur} type={props.password ? "password" : "text"} fullWidth={props.fullWidth} placeholder={props.placeholder} />
    <ValidationMessage field={props.name} fieldMessages={props.fieldMessages} isFormError={props.isFormError ? true : false} />
  </FormControl>
);

export default withStyles(allStyles)(TextFieldControl);