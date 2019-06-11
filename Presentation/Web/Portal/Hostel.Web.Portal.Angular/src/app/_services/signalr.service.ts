import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Person } from '../models/Person.Model';
import { Account } from '../models/Account.Model';
import { interval, Subject } from 'rxjs';
import { HttpTransportType, HubConnection, LogLevel, HubConnectionState } from '@aspnet/signalr';
import { CONFIGURATION } from '../shared/app.constants';
import { v4 as uuid } from 'uuid';
//import { PushEvent } from '../models/Event';
const WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS = 2000;
const commander = uuid();
@Injectable()
export class SignalRService {
  private token: string;
  private role: string;
  private hub: string;
  connectionEstablished = new Subject<Boolean>();
  private hubConnection: HubConnection;
  constructor() {
  }
  public createConnection(token: string, role: string, hub: string)
  {
    this.token = token; this.role = role; this.hub = hub;
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(CONFIGURATION.baseUrls.events + hub, HttpTransportType.WebSockets)
      .configureLogging(LogLevel.Debug)
      .build();
    switch (role.toLowerCase()) {
      case "owner":
        this.registerOwnerEvents(); break;
      case "tenant": this.registerTenantEvents(); break;
      default: break;
    }
    this.startConnection();
  }
  private startConnection() {
    setTimeout(() =>
    {
      this.hubConnection.onclose(() => {
        this.connectionEstablished.next(false);
        this.hubConnection.stop();
        this.createConnection(this.token, this.role, this.hub);
      });
      this.hubConnection.start().then(() => {
        console.log('Hub connection started');
        this.connectionEstablished.next(true);
      });
    }, WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS);
  }
  public registerOwnerEvents(): void
  {
    
  }
  public registerTenantEvents(): void {

  }
  public closeConnection(): void {
    this.hubConnection.stop();
  }
}
