import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Account } from '../../models/Account.Model';
@Component({
  selector: 'account-div',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.csss']
})
export class AccountComponent implements OnInit {
  ngOnInit() {
  }
}
