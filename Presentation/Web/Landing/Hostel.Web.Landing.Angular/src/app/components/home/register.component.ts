import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../../services/home.service';
import { SignalRService } from '../../services/signalr.service';
import { Person } from '../../models/Person.Model';
import { Router } from '@angular/router';
@Component({
  selector: 'register-div',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  responses: Observable<string>;
  person: Person = new Person();
  commander: string;
  connected: boolean;
  roles = ['Owner', 'Tenant'];
  constructor(private homeService: HomeService, private signalRService: SignalRService, public router: Router
  ) { }
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
    this.signalRService.commander.subscribe((id: string) => {
      this.commander = id;
    });
  }
  public RegisterPerson() {//this is AI folks ;)
    this.person.cmd = this.commander;
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
                  this.homeService.createPerson(this.person)
                    .subscribe(data => {
                      this.person = new Person();
                      let rep = JSON.parse(data);
                    });
                }
              }
            }
          }
        }
      }
    }
  }
}
