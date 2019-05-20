import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';

@Component({
  selector: 'home-div',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{
  constructor(public router: Router)
  { }
  public SignUp() {
    this.router.navigateByUrl('/register', { state: { commander: '' } });
  }
  public GoToPortal() {
    window.open("https://portal.hostel.com", "_blank")
  }
}
