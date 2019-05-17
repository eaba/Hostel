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
import { Person } from '../models/Person.Model';
import { Account } from '../models/Account.Model';
import { Transporter } from '../models/transporter.model';
import { CONFIGURATION } from '../shared/app.constants';
import { v4 as uuid } from 'uuid';

@Injectable()
export class HomeService {
  private personActionUrl: string;
  private accountActionUrl: string;

  constructor(private http: HttpClient) {
    this.personActionUrl = CONFIGURATION.baseUrls.api + 'person';
    this.accountActionUrl = CONFIGURATION.baseUrls.api + 'account';
  }
  public createPerson(person: Person): Observable<string> {
    let data = new Transporter();
    data.Commander = person.cmd;
    data.Command = "CreatePerson";
    data.CommandId = uuid();
    data.Payload = person;
    const jsonData: string = JSON.stringify({ payload: data });
    return this.http
      .post<string>(this.personActionUrl, jsonData)
      .pipe(catchError(this.handleError));
  }
  public createAccount(account: Account): Observable<string> {
    let data = new Transporter();
    data.Commander = account.cmd;
    data.Command = "CreateAccount";
    data.CommandId = uuid();
    data.Payload = account;
    const jsonData: string = JSON.stringify({ payload: data });

    return this.http
      .post<string>(this.accountActionUrl, jsonData)
      .pipe(catchError(this.handleError));
  }
  
  private handleError(error: Response) {
    return throwError(error || 'Server error');
  }
}
@Injectable()
export class HostelInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (!req.headers.has('Content-Type')) {
      req = req.clone({
        headers: req.headers.set('Content-Type', 'application/json')
      });
    }
    req = req.clone({ headers: req.headers.set('Accept', 'application/json') });
    return next.handle(req);
  }
}
