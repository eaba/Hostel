import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HomeService } from '../_services/home.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthObject } from '../_models/AuthObject';
@Component({
  selector: 'home-div',
  templateUrl: './home.component.html',
  providers: [HomeService]
})
export class HomeComponent implements OnInit {
  authObject: AuthObject = new AuthObject();
  constructor(private homeService: HomeService, public router: Router, private toastr: ToastrService) {
    let auth = JSON.parse(localStorage.getItem('authObject'));
    if (auth && auth.Token && auth.Role)
    {
      this.authObject.token = auth.Token;
      this.authObject.role = auth.Role;
      switch (auth.Role)
      {
        case 'owner':
          this.router.navigateByUrl('/owner', { state: this.authObject });
          break;
        case 'tenant':
          this.router.navigateByUrl('/tenant', { state: this.authObject });
          break;
        default:
          this.router.navigateByUrl('/404');
          break;
      }
    }
  }
  ngOnInit() {
    this.homeService.getAuthObject().subscribe((data: string) => {
      let auth = JSON.parse(data);
      if (auth && auth.Token && auth.Role)
      {
        this.authObject.token = auth.Token;
        this.authObject.role = auth.Role;
        localStorage.setItem('authObject', JSON.stringify(auth));
        switch (auth.Role) {
          case 'owner':
            this.router.navigateByUrl('/owner', { state: this.authObject });
            break;
          case 'tenant':
            this.router.navigateByUrl('/tenant', { state: this.authObject });
            break;
          default:
            this.router.navigateByUrl('/404');
            break;
        }
      }
      else {
        this.router.navigateByUrl('/unauthorized');
      }
    });
  }
}
