import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthObject } from '../_models/AuthObject';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentAuthSubject: BehaviorSubject<AuthObject>;
  public currentAuth: Observable<AuthObject>;

  constructor(private http: HttpClient) {
    this.currentAuthSubject = new BehaviorSubject<AuthObject>(JSON.parse(localStorage.getItem('currentAuth')));
    this.currentAuth = this.currentAuthSubject.asObservable();
  }

  public get currentAuthValue(): AuthObject {
    return this.currentAuthSubject.value;
  }

  login(username: string, password: string) {
    return this.http.post<any>(`${config.apiUrl}/users/authenticate`, { username, password })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }

        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentAuthSubject.next(null);
  }
}
