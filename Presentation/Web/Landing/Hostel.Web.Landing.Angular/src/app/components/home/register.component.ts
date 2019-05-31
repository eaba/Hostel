import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Person } from '../../models/Person.Model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { v4 as uuid } from 'uuid';
import { PushEvent } from '../../models/Event';
@Component({
  selector: 'register-div',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [HomeService, SignalRService]
})
export class RegisterComponent implements OnInit {
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
    this.signalRService.personCreated.subscribe((event: PushEvent) =>
    {
      console.log(event);
      if (event.Success) {
        let payload = JSON.parse(event.Payload);
        this.router.navigateByUrl('/account', { state: { email: payload.email, role: payload.role } });
      }
      else {
        this.toastr.error('Failed Registration', event.Error);
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
                    .subscribe(data =>
                    {
                      this.person = new Person();
                      this.toastr.info('Hi!', data);
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
