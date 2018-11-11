export interface FormValidationResult {
  fieldMessages: FormFieldValidationResult[],
  isFormError: boolean
}

export interface FormFieldValidationResult {
  isFieldError: boolean,
  fieldErrorMessage: string,
  fieldValidationType: FieldValidationType,
  field: string
}

export enum FieldValidationType {
  isCustom = 0,
  isRequired = 1
}

export interface FieldValidator {
  validationType: FieldValidationType,
  fieldName: string
}