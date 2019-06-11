import {
  HttpClient,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CONFIGURATION } from '../shared/app.constants';

@Injectable()
export class HomeService {
  private authObjectUrl: string;
  private logoutUrl: string;

  constructor(private http: HttpClient) {
    this.authObjectUrl = CONFIGURATION.baseUrls.home + 'authobject';
    this.logoutUrl = CONFIGURATION.baseUrls.home + 'logout';
  }
  public getAuthObject(): Observable<string> {
    return this.http.get<string>(this.authObjectUrl)
      .pipe(catchError(this.handleError));
  }
  public logout(account: string): Observable<string> {
    return this.http.get<string>(this.logoutUrl)
      .pipe(catchError(this.handleError));
  }
  
  private handleError(error: Response) {
    console.log(error);
    return throwError(error || 'Server error');
  }
}

