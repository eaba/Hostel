import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Person } from '../models/Person.Model';
import { Account } from '../models/Account.Model';
import { interval, Subject } from 'rxjs';
import { HttpTransportType, HubConnection, LogLevel } from '@aspnet/signalr';
import { CONFIGURATION } from '../shared/app.constants';
import { v4 as uuid } from 'uuid';
import { PushEvent } from '../models/Event';
const WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS = 2000;
const commander = uuid();
@Injectable()
export class SignalRService {
  personCreated = new Subject<PushEvent>();
  accountCreated = new Subject<PushEvent>();
  connectionEstablished = new Subject<Boolean>();
  private hubConnection: HubConnection;
  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
    //this.commander = uuid();
  }
  public createConnection()
  {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(CONFIGURATION.baseUrls.events + "home?commander=" + commander, HttpTransportType.WebSockets)
      .configureLogging(LogLevel.Debug)
      .build();
  }
  private startConnection() {
    setTimeout(() =>
    {
      this.hubConnection.onclose(() => this.connectionEstablished.next(false));
      this.hubConnection.start().then(() => {
        console.log('Hub connection started');
        this.connectionEstablished.next(true);
      });
    }, WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS);
  }
  public registerOnServerEvents(): void
  {
    this.hubConnection.on('personcreated', (payload: any, id: string, success: boolean, error: string) => {
      let pEvent = new PushEvent();
      pEvent.Success = success;
      pEvent.Id = id;
      pEvent.Error = error;
      pEvent.Payload = payload;
      this.personCreated.next(pEvent);
    });
    this.hubConnection.on('accountcreated', (payload: any, id: string, success: boolean, error: string) => {
      let pEvent = new PushEvent();
      pEvent.Success = success;
      pEvent.Id = id;
      pEvent.Error = error;
      pEvent.Payload = payload;
      this.accountCreated.next(pEvent);
    });
  }
  public GetCommander(): string {
    return commander;
  }
}
