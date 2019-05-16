import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Person } from '../../models/Person.Model';
import { Account } from '../../models/Account.Model';
import { trigger, state, style, transition, animate } from '@angular/animations'

@Component({
  selector: 'home-div',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  responses: Observable<string>;
  person: Person = new Person();
  account: Account = new Account();
  commander: string;
  connected: boolean;
  constructor(
    private dataService: HomeService,
    private signalRService: SignalRService
  ) { }

  ngOnInit() {
    this.subscribeToEvents();
    this.signalRService.connectionEstablished.subscribe((state: boolean) => {
      this.connected = state
    });
  }
  private subscribeToEvents(): void {
    this.signalRService.serverData.subscribe((data: any) => {
      let response = JSON.parse(data);
    });
    this.signalRService.commander.subscribe((id: string) => {
      this.commander = id;
    });
  }
}
