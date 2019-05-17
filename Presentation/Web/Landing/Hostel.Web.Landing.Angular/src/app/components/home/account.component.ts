import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Account } from '../../models/Account.Model';
import { Router } from '@angular/router';
@Component({
  selector: 'account-div',
  templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit {
  account: Account = new Account();
  responses: Observable<string>;
  commander: string;
  connected: boolean;
  constructor(private homeService: HomeService, private signalRService: SignalRService, public router: Router) {

  }
  ngOnInit() {
    let data = JSON.parse(history.state.data);
    this.account.email = data.email;
    this.account.role = data.role;
    this.subscribeToEvents();
    this.signalRService.connectionEstablished.subscribe((state: boolean) => {
      this.connected = state;
    });
  }
  private subscribeToEvents(): void {
    this.signalRService.serverData.subscribe((data: any) => {
      let response = JSON.parse(data);
      let cmd = response.Command;
      if (cmd === 'AccountCreated') {
        //alert with message
        window.open("https://portal.hostel.com", "_blank");
      }
    });
    this.signalRService.commander.subscribe((id: string) => {
      this.commander = id;
    });
  }
  public RegisterAccount() {//this is AI folks ;)
    this.account.cmd = this.commander;
    if (this.account.cmd) {
      if (this.account.confirm === this.account.password) {
        if (this.account.email) {
          if (this.account.role) {
            this.homeService.createAccount(this.account)
              .subscribe(data => {
                this.account = new Account();
                let rep = JSON.parse(data);
              });
          }            
        }
      }
    }
  }
}
