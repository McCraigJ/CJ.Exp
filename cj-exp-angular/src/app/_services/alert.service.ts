import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, SimpleSnackBar } from '@angular/material/snack-bar';
import { Router, NavigationStart } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AlertService {

    private snackBarRef: MatSnackBarRef<SimpleSnackBar>;
    private keepOpenOnNavigate: boolean = false;

    constructor(
        private snackBar: MatSnackBar,
        private router: Router
    ) {
        this.router.events.subscribe(evt => {
            if (evt instanceof NavigationStart) {
                if (this.keepOpenOnNavigate) {
                    this.keepOpenOnNavigate = false;
                } else {
                    this.clear();
                }
            }
        });
    }

    openSnackBar(message: string, action: string, duration: number, cssClass: string) {
        this.clear();

        this.snackBarRef = this.snackBar.open(message, action, {
            duration,
            panelClass: cssClass
        });
    };
    
    public success(message: string) {
        this.keepOpenOnNavigate = true;
        this.openSnackBar(message, 'close', 5000, 'snack-success');
    }

    public error(message: string) {
        this.openSnackBar(message, 'close', 100000, 'snack-error');
    }

    public clear() {
        if (this.snackBarRef !== undefined) {
            this.snackBarRef.dismiss();
        }
    }
}