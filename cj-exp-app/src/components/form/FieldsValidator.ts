import { FieldValidationType, FieldValidator, FormFieldValidationResult, FormValidationResult } from './types';

export function ValidateFields(validationConfig: FieldValidator[], obj: any): FormValidationResult {

  const res: FormValidationResult = { fieldMessages: [], isFormError: false };

    validationConfig.forEach(x => {

    let fieldVal:string = "";
    if (typeof obj === 'string' || obj instanceof String) {
      fieldVal = obj.toString();
    }  
    else {
      fieldVal = obj[x.fieldName];
    }
    
    if (fieldVal !== undefined) {  
      const fieldValidationResult = PerformFieldValidation(x.validationType, x.fieldName, fieldVal.toString());

      if (fieldValidationResult.isFieldError) {
        res.fieldMessages.push(fieldValidationResult);
        res.isFormError = true;
      }
    }
  });  

  return res;
}

export function ValidateField(validationConfig: FieldValidator[], fieldName: string, obj: any, currentFormValidation:FormValidationResult): FormValidationResult {  

  let updatedFormValidation: FormValidationResult = currentFormValidation;

  validationConfig.forEach(x => {
    if (x.fieldName === fieldName) {
      const fieldVal = obj[fieldName];
      if (fieldVal !== undefined) {
        const fieldValidationResult = PerformFieldValidation(x.validationType, fieldName, fieldVal.toString());
        updatedFormValidation = updateFormValidationResult(fieldValidationResult, updatedFormValidation);
      }
    }    
  });
  return updatedFormValidation;  
}

function updateFormValidationResult(fieldValidationResult:FormFieldValidationResult, currentFormValidation:FormValidationResult):FormValidationResult {
  
  const messageIndex:number = currentFormValidation.fieldMessages.findIndex((e) => {
    return e.field === fieldValidationResult.field && e.fieldValidationType === fieldValidationResult.fieldValidationType;
  });

  if (fieldValidationResult.isFieldError) {
    if (messageIndex === -1) {
      return {...currentFormValidation, fieldMessages : [ ...currentFormValidation.fieldMessages, fieldValidationResult], isFormError: true };
    } else {
      return currentFormValidation;
    }
    
  } else {

    if (messageIndex >= 0) {
      
      const updatedFormValidationMessages = 
        [...currentFormValidation.fieldMessages.slice(0, messageIndex), 
          ...currentFormValidation.fieldMessages.slice(messageIndex + 1)];
      
      const updatedFormValidation: FormValidationResult = {
        fieldMessages: updatedFormValidationMessages,
        isFormError: currentFormValidation.fieldMessages.length > 0
      };

      return updatedFormValidation;  
    }

    return currentFormValidation;
  }
}

function PerformFieldValidation(validationType:FieldValidationType, fieldName:string, fieldVal:string):FormFieldValidationResult {

  let res:FormFieldValidationResult = { fieldErrorMessage: "", isFieldError: false, field: fieldName, fieldValidationType: validationType };

  switch (validationType) {
    case FieldValidationType.isRequired:
      if (fieldVal === "") {          
        res = {
          field: fieldName,
          isFieldError: true,
          fieldValidationType: validationType,
          fieldErrorMessage: "Field is required"
        };
      }
      break;    
  }    
  return res;
}