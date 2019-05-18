import { BrowserModule } from '@angular/platform-browser';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HomeService } from '../app/services/home.service';
import { SignalRService } from '../app/services/signalr.service';

import { AppComponent } from './app.component';
import { HomeComponent } from '../app/components/home/home.component';
import { RegisterComponent } from '../app/components/home/register.component';
import { AccountComponent } from '../app/components/home/account.component';

@NgModule({
  declarations: [
    AppComponent,
	  HomeComponent,
	  RegisterComponent,
	  AccountComponent
  ],
  imports: [
    AngularFontAwesomeModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AppComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'account', component: AccountComponent },
    ])
  ],
  providers: [HomeService, SignalRService],
  bootstrap: [AppComponent]
})
export class AppModule { }
