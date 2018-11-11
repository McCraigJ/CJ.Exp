import { createStyles, FormControl, InputLabel, MenuItem, Select, Theme, WithStyles, withStyles } from '@material-ui/core';
import * as React from 'react';
import { styles } from '../AppTheme';
import { FormFieldValidationResult } from '../components/form/types';
import { ValidationMessage } from '../components/form/ValidationMessage';

interface SelectFieldControlType {
  name: string,
  label: string,
  value: string,
  required?: boolean,
  onChange?: (event: any) => void,
  onBlur?: (event: any) => void,
  isFormError?: boolean,
  fieldMessages: FormFieldValidationResult[],
  fullWidth?: boolean,
  options: SelectFieldOptions[]
  addBlankOption?: boolean
}

export interface SelectFieldOptions {
  text: string,
  value: string
}

// Join a local style with a higher level style
const allStyles = (theme: Theme) => createStyles({
  ...styles(theme)
});

const SelectFieldControl: React.SFC<SelectFieldControlType & WithStyles<typeof allStyles>> = (props) => (
  <FormControl className={props.fullWidth ? props.classes.fullWidth : props.classes.standardWidth}>
    <InputLabel htmlFor={props.name}>{props.label}</InputLabel>
    <Select fullWidth value={props.value} onChange={props.onChange} id={props.name} name={props.name}>
      {props.addBlankOption ? <MenuItem /> : ""}
      {
        props.options.map(o => {
          return <MenuItem key={o.value} value={o.value}>{o.text}</MenuItem>
        })
      }
    </Select>
    <ValidationMessage field={name} fieldMessages={props.fieldMessages} isFormError={props.isFormError ? true : false} />
  </FormControl>
);

export default withStyles(allStyles)(SelectFieldControl);