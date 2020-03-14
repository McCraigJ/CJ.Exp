import { AbstractControl } from '@angular/forms';

export function currencyValidator(control: AbstractControl) {
    if (control.value <= 0) {
        return { currencyInvalid: true };        
    }
    return null;
}