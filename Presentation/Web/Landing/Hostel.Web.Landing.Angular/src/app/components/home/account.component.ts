import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Account } from '../../models/Account.Model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { v4 as uuid } from 'uuid';
import { PushEvent } from '../../models/Event';
@Component({
  selector: 'account-div',
  templateUrl: './account.component.html',
  providers: [HomeService, SignalRService]
})
export class AccountComponent implements OnInit {
  account: Account = new Account();
  responses: Observable<string>;
  commander: string;
  connected: boolean;
  constructor(private homeService: HomeService, private signalRService: SignalRService, public router: Router, private toastr: ToastrService) {

  }
  ngOnInit() {
    let data = history.state;
    console.log(data.email);
    this.account.email = data.email;
    this.account.role = data.role;
    this.subscribeToEvents();
    this.signalRService.connectionEstablished.subscribe((state: boolean) => {
      this.connected = state;
    });
  }
  private subscribeToEvents(): void {
    this.signalRService.accountCreated.subscribe((event: PushEvent) => {
      console.log(event);
      if (event.Success)
      {
        window.open("https://portal.hostel.com", "_blank");
      }
      else {
        this.toastr.error(event.Error, 'Account Creation Failed', { timeOut: 15000, positionClass: 'toast-top-center' });
      }
    });
  }
  public RegisterAccount() {//this is AI folks ;)
    this.account.cmd = this.signalRService.GetCommander();
    if (this.account.cmd)
    {
      if (this.account.confirm === this.account.password)
      {
        if (this.account.email)
        {
          if (this.account.role)
          {
            let data = { Commander: this.account.cmd, Command: "CreateAccount", CommandId: uuid(), Payload: JSON.stringify({ Email: this.account.email, Password: this.account.password, Role: this.account.role, Phone: this.account.phone }) };
            this.homeService.createAccount(JSON.stringify(data))
              .subscribe(data =>
              {
                this.account = new Account();
                this.toastr.info(data, 'Hi!', { timeOut: 15000, positionClass: 'toast-top-center' });
              });
          }            
        }
      }
    }
    //console.log(this.account);
  }
}
