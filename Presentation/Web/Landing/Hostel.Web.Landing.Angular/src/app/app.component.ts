import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { SignalRService } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'Hostel';
  constructor(private router: Router) { }

  ngOnInit() {
    this.router.navigateByUrl('/home', { state: { commander: '' } });
  }
}
