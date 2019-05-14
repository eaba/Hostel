import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Person } from '../models/Person.Model';
import { Account } from '../models/Account.Model';
import { interval } from 'rxjs';
import { HttpTransportType } from '@aspnet/signalr';

@Injectable()
export class SignalRService {
  public person: Person;
  public account: Account;
  public connected: boolean = false;

  private hubConnection: signalR.HubConnection;
  public startConnection = (url: string) =>
  {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url, HttpTransportType.WebSockets)
      .build();
    this.hubConnection.onclose(() =>
    {
      this.connected = false;
      this.startReconnection(url);
    });
    this.hubConnection.start()
      .then(() => { console.log('Connection started'); })
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        this.startReconnection(url);
      })
  }
  private startReconnection(url: string) {
    console.log('Connection started');
    setTimeout(()=> this.startConnection, 2000)
  }
  public addListeners = () => {

    this.hubConnection.on('personcreated', (data: string) => {
      console.log(data);
    });
    this.hubConnection.on('accountcreated', (data: string) => {
      console.log(data);
    });
    this.hubConnection.on('connected', (data: string) => {
      this.connected = true;
    });

  }
}
