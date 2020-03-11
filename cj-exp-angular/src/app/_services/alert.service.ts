import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, SimpleSnackBar } from '@angular/material/snack-bar';
import { Router, NavigationStart } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AlertService {

    private snackBarRef: MatSnackBarRef<SimpleSnackBar>;

    constructor(
        private snackBar: MatSnackBar,
        private router: Router
    ) {
        this.router.events.subscribe(evt => {
            if (evt instanceof NavigationStart) {
                this.clear();
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

    // Create a success message. Keep After Route Change defaults to false
    public success(message: string) {
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