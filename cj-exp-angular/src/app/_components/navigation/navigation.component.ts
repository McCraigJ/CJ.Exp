import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
// import './navigation.component.less';
// import { MatToolbar } from '@angular/material/toolbar';

// import { AuthenticationService } from '../../_services';
// import { User } from '../../_models';
// , styleUrls: ['./navigation.component.less']

@Component({ selector: 'app-navigation', templateUrl: 'navigation.component.html' })

export class NavigationComponent {
    // @Input()
    // currentUser: User;

    constructor(
        // private router: Router,
        // private authenticationService: AuthenticationService
    ) {
        // this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    logout() {
        // this.authenticationService.logout();
        // this.router.navigate(['/login']);
    }
}
