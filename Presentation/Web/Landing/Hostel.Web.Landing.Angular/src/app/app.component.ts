import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RouterOutlet, Router } from '@angular/router';
import { SignalRService } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'Hostel';
  constructor(private signalRService: SignalRService, public router: Router) { }

  ngOnInit() {
    this.signalRService.connectionEstablished.subscribe((state: boolean) => {
      if (state) {
        this.router.navigateByUrl('/home', { state: { commander: '' } });
      }
    });
  }
}
