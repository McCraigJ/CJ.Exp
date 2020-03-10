import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map, first } from 'rxjs/operators';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../_models/apiResponse';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username, password) {
        return this.http.post<ApiResponse>(`${environment.apiUrl}users/Login`, { email: username, password })
            .pipe(map(apiResponse => {
                if (apiResponse.success) {
                    const user: User = apiResponse.data;
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                    return user;
                }
            }));
    }

    logout(): Observable<any> {
        return this.http.post<ApiResponse>(`${environment.apiUrl}users/Logout`, '').pipe(first());
    }

    clearCurrentUser() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}
