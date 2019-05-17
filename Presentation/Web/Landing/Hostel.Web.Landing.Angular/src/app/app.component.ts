import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RouterOutlet, Router } from '@angular/router';
import { SignalRService } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Hostel';
  responses: Observable<string>;
  commander: string;
  connected: boolean;
  constructor(private signalRService: SignalRService, public router: Router
  ) { }

  ngOnInit() {
    this.subscribeToEvents();
    this.signalRService.connectionEstablished.subscribe((state: boolean) => {
      if (state) {
        this.router.navigateByUrl('/home', { state: { commander: '' } });
      }
    });
  }
  private subscribeToEvents(): void {
    this.signalRService.serverData.subscribe((data: any) => {
      let response = JSON.parse(data);
    });
    this.signalRService.commander.subscribe((id: string) => {
      
    });
  }
}
