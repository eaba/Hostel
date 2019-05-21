import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Person } from '../../models/Person.Model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { v4 as uuid } from 'uuid';
@Component({
  selector: 'register-div',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [HomeService, SignalRService]
})
export class RegisterComponent implements OnInit {
  responses: Observable<string>;
  person: Person = new Person();
  commander: string;
  connected: boolean;
  roles: string[];
  constructor(private homeService: HomeService, private signalRService: SignalRService, public router: Router, private toastr: ToastrService) {
    this.roles = ['Owner', 'Tenant'];
  }
  ngOnInit() {
    this.subscribeToEvents();
    this.signalRService.connectionEstablished.subscribe((state: boolean) => {
      this.connected = state;
    });
  }
  private subscribeToEvents(): void {
    this.signalRService.serverData.subscribe((data: any) => {
      let response = JSON.parse(data);
      let cmd = response.Command;
      if (cmd === 'PersonCreated') {
        this.router.navigateByUrl('/account', { state: { email: response.Email, role:response.Role } });
      }
    });
  }
  public RegisterPerson() {//this is AI folks ;)
    this.person.cmd = this.signalRService.GetCommander();
    if (this.person.cmd)
    {
      if (this.person.birthday)
      {
        if (this.person.email)
        {
          if (this.person.firstName)
          {
            if (this.person.lastName)
            {
              if (this.person.phone)
              {
                if (this.person.role)
                {
                  let data = { Commander: this.person.cmd, Command: "CreateAccount", CommandId: uuid(), Payload: JSON.stringify(this.person) };
                  this.homeService.createPerson(JSON.stringify(data))
                    .subscribe(data => {
                      this.person = new Person();
                      let rep = JSON.parse(data);
                      //this.toastr.success('Hello world!', 'Toastr fun!');
                    });
                }
              }
            }
          }
        }
      }
    }
    console.log(JSON.stringify(this.person));
  }
}
