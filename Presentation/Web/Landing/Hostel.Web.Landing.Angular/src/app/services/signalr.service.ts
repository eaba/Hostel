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
    this.hubConnection.on('personcreated', (event: string) => {
      let json = JSON.parse(event);
      let evnt = new PushEvent();
      evnt.Success = json.Success;
      evnt.Id = json.Id;
      evnt.Error = json.Error;
      evnt.Payload = json.Payload;
      this.personCreated.next(evnt);
    });
    this.hubConnection.on('accountcreated', (event: string) => {
      let json = JSON.parse(event);
      let evnt = new PushEvent();
      evnt.Success = json.Success;
      evnt.Id = json.Id;
      evnt.Error = json.Error;
      evnt.Payload = json.Payload;
      this.accountCreated.next(evnt);
    });
  }
  public GetCommander(): string {
    return commander;
  }
}
