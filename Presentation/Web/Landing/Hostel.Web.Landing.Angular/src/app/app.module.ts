import { BrowserModule } from '@angular/platform-browser';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { CommonModule, LocationStrategy, HashLocationStrategy, APP_BASE_HREF } from '@angular/common';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { HomeService } from '../app/services/home.service';
import { SignalRService } from '../app/services/signalr.service';

import { AppComponent } from './app.component';
import { HomeComponent } from '../app/components/home/home.component';
import { RegisterComponent } from '../app/components/home/register.component';
import { AccountComponent } from '../app/components/home/account.component';
import { HostelErrorHandler } from './providers/errorhandler';

@NgModule({
  declarations: [
    AppComponent,
	  HomeComponent,
	  RegisterComponent,
	  AccountComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule,
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
  providers: [{ provide: ErrorHandler, useClass: HostelErrorHandler }, { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: APP_BASE_HREF, useValue: '/' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
