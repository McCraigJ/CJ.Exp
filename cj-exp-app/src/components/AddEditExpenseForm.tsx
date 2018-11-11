import { Typography } from '@material-ui/core';
import * as React from 'react';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import ButtonControl from '../controls/ButtonControl';
import SelectFieldControl, { SelectFieldOptions } from '../controls/SelectFieldControl';
import TextFieldControl from '../controls/TextFieldControl';
import { ApplicationState } from '../store';
import { addExpense, editExpense } from '../store/expenses/actions';
import { FilterExpenses, getBlankExpense } from '../store/expenses/expenseDataHelpers';
import { Expense, ExpenseType } from '../store/expenses/types';
import { ValidateField, ValidateFields } from './form/FieldsValidator';
import { FieldValidationType, FieldValidator, FormValidationResult } from './form/types';

interface AddEditExpenseState {
  expense: Expense,
  expenseTypes: ExpenseType[],
  validatorConfig: FieldValidator[],
  validation: FormValidationResult  
}

interface AddEditExpenseFormProps {  
  expenseId: string,  
  onCompleteForm?: (() => void),
  onCancelForm?: (() => void)
}

interface PropsFromState {
  expense: Expense,
  expenseTypes: ExpenseType[],
}

interface PropsFromDispatch { 
  addExpense: typeof addExpense;
  editExpense: typeof editExpense;
}

type FormProps = AddEditExpenseFormProps & PropsFromState & PropsFromDispatch ;

class AddEditExpenseForm extends React.Component<FormProps, AddEditExpenseState> {
  constructor(props:FormProps) {
    super(props);

    // this.state = { ...this.state, validation: { fieldMessages: [], isFormError: false }, validatorConfig: this.configureValidator()};    
    this.state = {
      validation: { fieldMessages: [], isFormError: false},
      validatorConfig: this.configureValidator(),
      expense: props.expense,
      expenseTypes: props.expenseTypes
    };

    this.handleOnSubmit = this.handleOnSubmit.bind(this);
    this.handleOnCancel = this.handleOnCancel.bind(this);
    this.handleOnChange = this.handleOnCancel.bind(this);
    this.validateField = this.validateField.bind(this);
  }

  public render() {
    const isNew = this.props.expenseId === "";
    const { expenseTypeId, amount, description } = this.state.expense;
    return (
      <React.Fragment>
        <Typography variant="headline">{ isNew ? "Add " : "Edit"} Expense</Typography>

        <form noValidate autoComplete="off" onSubmit={this.handleOnSubmit}>
        <div className="form-group">          

          <SelectFieldControl name="expenseTypeId" label="Expense Type" value={expenseTypeId.toString()} required onChange={this.handleOnChange} 
            onBlur={this.validateField} fieldMessages={this.state.validation.fieldMessages} isFormError={this.state.validation.isFormError}
            options={this.getExpenseTypeOptions()} />

          <TextFieldControl name="amount" label="Amount" value={amount.toLocaleString()} required onChange={this.handleOnChange} 
            onBlur={this.validateField} fieldMessages={this.state.validation.fieldMessages} isFormError={this.state.validation.isFormError} />          

          <TextFieldControl name="description" label="Description" value={description} onChange={this.handleOnChange}
            fieldMessages={[]}  />  
          

        </div>        
        <ButtonControl variant="contained" color="primary" onClick={this.handleOnSubmit}>{ isNew ? "Save" : "Update"}</ButtonControl>
        <ButtonControl variant="contained" color="secondary" onClick={this.handleOnCancel}>Cancel</ButtonControl>        
      </form>

      </React.Fragment>
    );
  }

  private handleOnChange(event: any) : void {        
    this.setState({ ...this.state, expense: {...this.state.expense, [event.target.name] : event.target.value }});
  }

  private handleOnSubmit() {    
    const isValid = this.validateForm();
        
    if (isValid) {
      const { expense } = this.state;
      if (expense.id === "") {
        this.props.addExpense(expense);
      } else {
        this.props.editExpense(expense);
      }
      if (this.props.onCompleteForm) {
        this.props.onCompleteForm();
      }
      
      this.resetForm();
    }    
  }  

  private handleOnCancel() {    
    if (this.props.onCancelForm) {
      this.props.onCancelForm();
    }
    this.resetForm();
  }

  private resetForm() {
    this.setState({...this.state, expense: getBlankExpense()});
  }
  
  private configureValidator():FieldValidator[] {
    return [{
      fieldName: "expenseTypeId",
      validationType: FieldValidationType.isRequired
    },
    {
      fieldName: "amount",
      validationType: FieldValidationType.isRequired
    }];
  }

  private getExpenseTypeOptions(): SelectFieldOptions[] {

    const options: SelectFieldOptions[] = [];

    this.state.expenseTypes.forEach(t => {
      options.push({
        text: t.name,
        value: t.id
      });
    });

    return options;
  }

  private validateForm():boolean {    
    const result:FormValidationResult = ValidateFields(this.state.validatorConfig, this.state.expense);
    this.setState({ ...this.state, validation: result});    
    return !result.isFormError;
  }

  private validateField(e:any) {    
    const result:FormValidationResult = ValidateField(this.state.validatorConfig, e.target.id, this.state.expense, this.state.validation);    
    this.setState({ ...this.state, validation: result});
  }  
}

const mapStateToProps = ({ expenseState }: ApplicationState, params: AddEditExpenseFormProps) => ({  
  expense: FilterExpenses(expenseState.expenses, params.expenseId),
  expenseTypes: expenseState.expenseTypes
});

const mapDispatchToProps = (dispatch: Dispatch) => ({  
  addExpense: (data: Expense) => dispatch(addExpense(data)),
  editExpense: (data: Expense) => dispatch(editExpense(data))
});

export default connect(mapStateToProps, mapDispatchToProps)(AddEditExpenseForm);