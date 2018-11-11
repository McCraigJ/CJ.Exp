import { FormHelperText } from '@material-ui/core';
import * as React from 'react';
import { FormValidationResult } from './types';

interface CurrentField {
  field: string
};

export class ValidationMessage extends React.Component<CurrentField & FormValidationResult> {

  constructor(props:CurrentField & FormValidationResult) {
    super(props);
  }

  public render() {

    const { field, fieldMessages } = this.props;

    const fieldMessage = fieldMessages.filter(m => m.field === field);

    if (fieldMessage !== null && fieldMessage.length === 1) {
      return (
        <FormHelperText error>{fieldMessage[0].fieldErrorMessage}</FormHelperText>
      )  
    }
    return (
      <span />
    )    
  }
}