(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\r\n  <router-outlet  #outlet=\"outlet\"></router-outlet>\r\n"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var AppComponent = /** @class */ (function () {
    function AppComponent(router) {
        this.router = router;
        this.title = 'Hostel';
    }
    AppComponent.prototype.ngOnInit = function () {
        this.router.navigateByUrl('/home', { state: { commander: '' } });
    };
    AppComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-root',
            template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html")
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], AppComponent);
    return AppComponent;
}());



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser/animations */ "./node_modules/@angular/platform-browser/fesm5/animations.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var angular_font_awesome__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! angular-font-awesome */ "./node_modules/angular-font-awesome/dist/angular-font-awesome.es5.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _app_components_home_home_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ../app/components/home/home.component */ "./src/app/components/home/home.component.ts");
/* harmony import */ var _app_components_home_register_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../app/components/home/register.component */ "./src/app/components/home/register.component.ts");
/* harmony import */ var _app_components_home_account_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../app/components/home/account.component */ "./src/app/components/home/account.component.ts");
/* harmony import */ var _providers_errorhandler__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./providers/errorhandler */ "./src/app/providers/errorhandler.ts");
/* harmony import */ var _providers_httpinterceptor__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./providers/httpinterceptor */ "./src/app/providers/httpinterceptor.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};















var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_5__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_9__["AppComponent"],
                _app_components_home_home_component__WEBPACK_IMPORTED_MODULE_10__["HomeComponent"],
                _app_components_home_register_component__WEBPACK_IMPORTED_MODULE_11__["RegisterComponent"],
                _app_components_home_account_component__WEBPACK_IMPORTED_MODULE_12__["AccountComponent"]
            ],
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_4__["CommonModule"],
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__["BrowserModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_8__["RouterModule"],
                angular_font_awesome__WEBPACK_IMPORTED_MODULE_3__["AngularFontAwesomeModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_7__["HttpClientModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_6__["FormsModule"],
                _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_1__["BrowserAnimationsModule"],
                ngx_toastr__WEBPACK_IMPORTED_MODULE_2__["ToastrModule"].forRoot(),
                _angular_router__WEBPACK_IMPORTED_MODULE_8__["RouterModule"].forRoot([
                    { path: '', component: _app_component__WEBPACK_IMPORTED_MODULE_9__["AppComponent"], pathMatch: 'full' },
                    { path: 'home', component: _app_components_home_home_component__WEBPACK_IMPORTED_MODULE_10__["HomeComponent"] },
                    { path: 'register', component: _app_components_home_register_component__WEBPACK_IMPORTED_MODULE_11__["RegisterComponent"] },
                    { path: 'account', component: _app_components_home_account_component__WEBPACK_IMPORTED_MODULE_12__["AccountComponent"] },
                ])
            ],
            providers: [{ provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_7__["HTTP_INTERCEPTORS"], useClass: _providers_httpinterceptor__WEBPACK_IMPORTED_MODULE_14__["HostelInterceptor"], multi: true }, { provide: _angular_core__WEBPACK_IMPORTED_MODULE_5__["ErrorHandler"], useClass: _providers_errorhandler__WEBPACK_IMPORTED_MODULE_13__["HostelErrorHandler"] }, { provide: _angular_common__WEBPACK_IMPORTED_MODULE_4__["LocationStrategy"], useClass: _angular_common__WEBPACK_IMPORTED_MODULE_4__["HashLocationStrategy"] },
                { provide: _angular_common__WEBPACK_IMPORTED_MODULE_4__["APP_BASE_HREF"], useValue: '/' }],
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_9__["AppComponent"]]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/app/components/home/account.component.html":
/*!********************************************************!*\
  !*** ./src/app/components/home/account.component.html ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form class=\"animated zoomIn\">\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"password\">Password</label>\r\n      <input type=\"password\" name=\"password\" class=\"form-control\" id=\"password\" placeholder=\"Password\" [(ngModel)]=\"account.password\">\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"confirm\">Confirm Password</label>\r\n      <input type=\"password\" name=\"confirm\" class=\"form-control\" id=\"confirm\" placeholder=\"Confirm Password\" [(ngModel)]=\"account.confirm\">\r\n    </div>\r\n  </div>\r\n  <input class=\"btn btn-success\" style=\"cursor:pointer\" (click)=\"RegisterAccount()\" value=\"Submit!\" [hidden]=\"!connected\">\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/components/home/account.component.ts":
/*!******************************************************!*\
  !*** ./src/app/components/home/account.component.ts ***!
  \******************************************************/
/*! exports provided: AccountComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccountComponent", function() { return AccountComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_home_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/home.service */ "./src/app/services/home.service.ts");
/* harmony import */ var _services_signalr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/signalr.service */ "./src/app/services/signalr.service.ts");
/* harmony import */ var _models_Account_Model__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../models/Account.Model */ "./src/app/models/Account.Model.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var AccountComponent = /** @class */ (function () {
    function AccountComponent(homeService, signalRService, router, toastr) {
        this.homeService = homeService;
        this.signalRService = signalRService;
        this.router = router;
        this.toastr = toastr;
        this.account = new _models_Account_Model__WEBPACK_IMPORTED_MODULE_3__["Account"]();
    }
    AccountComponent.prototype.ngOnInit = function () {
        var _this = this;
        var data = JSON.parse(history.state.data);
        this.account.email = data.email;
        this.account.role = data.role;
        this.subscribeToEvents();
        this.signalRService.connectionEstablished.subscribe(function (state) {
            _this.connected = state;
        });
    };
    AccountComponent.prototype.subscribeToEvents = function () {
        var _this = this;
        this.signalRService.accountCreated.subscribe(function (event) {
            console.log(event);
            if (event.Success) {
                var payload = JSON.parse(event.Payload);
                window.open("https://portal.hostel.com", "_blank");
            }
            else {
                _this.toastr.error(event.Error, 'Account Creation Failed', { timeOut: 15000, positionClass: 'toast-top-center' });
            }
        });
    };
    AccountComponent.prototype.RegisterAccount = function () {
        var _this = this;
        this.account.cmd = this.signalRService.GetCommander();
        if (this.account.cmd) {
            if (this.account.confirm === this.account.password) {
                if (this.account.email) {
                    if (this.account.role) {
                        this.homeService.createAccount(this.account)
                            .subscribe(function (data) {
                            _this.account = new _models_Account_Model__WEBPACK_IMPORTED_MODULE_3__["Account"]();
                            var rep = JSON.parse(data);
                            //this.toastr.success('Hello world!', 'Toastr fun!');
                        });
                    }
                }
            }
        }
        console.log(this.account);
    };
    AccountComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'account-div',
            template: __webpack_require__(/*! ./account.component.html */ "./src/app/components/home/account.component.html"),
            providers: [_services_home_service__WEBPACK_IMPORTED_MODULE_1__["HomeService"], _services_signalr_service__WEBPACK_IMPORTED_MODULE_2__["SignalRService"]]
        }),
        __metadata("design:paramtypes", [_services_home_service__WEBPACK_IMPORTED_MODULE_1__["HomeService"], _services_signalr_service__WEBPACK_IMPORTED_MODULE_2__["SignalRService"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"], ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"]])
    ], AccountComponent);
    return AccountComponent;
}());



/***/ }),

/***/ "./src/app/components/home/home.component.css":
/*!****************************************************!*\
  !*** ./src/app/components/home/home.component.css ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "body {\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvY29tcG9uZW50cy9ob21lL2hvbWUuY29tcG9uZW50LmNzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtBQUNBIiwiZmlsZSI6InNyYy9hcHAvY29tcG9uZW50cy9ob21lL2hvbWUuY29tcG9uZW50LmNzcyIsInNvdXJjZXNDb250ZW50IjpbImJvZHkge1xyXG59XHJcbiJdfQ== */"

/***/ }),

/***/ "./src/app/components/home/home.component.html":
/*!*****************************************************!*\
  !*** ./src/app/components/home/home.component.html ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form class=\"animated bounceInDown\">\r\n  <div class=\"form-row\" style=\"margin-bottom:50px\">\r\n    <div class=\"form-group\">\r\n      <button class=\"btn btn-primary\" (click)=\"SignUp()\">Sign Up</button>\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group\">\r\n      <button class=\"btn btn-primary\" (click)=\"GoToPortal()\">Portal</button>\r\n    </div>\r\n  </div>\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/components/home/home.component.ts":
/*!***************************************************!*\
  !*** ./src/app/components/home/home.component.ts ***!
  \***************************************************/
/*! exports provided: HomeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HomeComponent", function() { return HomeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HomeComponent = /** @class */ (function () {
    function HomeComponent(router) {
        this.router = router;
    }
    HomeComponent.prototype.SignUp = function () {
        this.router.navigateByUrl('/register', { state: { commander: '' } });
    };
    HomeComponent.prototype.GoToPortal = function () {
        window.open("https://portal.hostel.com", "_blank");
    };
    HomeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'home-div',
            template: __webpack_require__(/*! ./home.component.html */ "./src/app/components/home/home.component.html"),
            styles: [__webpack_require__(/*! ./home.component.css */ "./src/app/components/home/home.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], HomeComponent);
    return HomeComponent;
}());



/***/ }),

/***/ "./src/app/components/home/register.component.css":
/*!********************************************************!*\
  !*** ./src/app/components/home/register.component.css ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "body {\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvY29tcG9uZW50cy9ob21lL3JlZ2lzdGVyLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7QUFDQSIsImZpbGUiOiJzcmMvYXBwL2NvbXBvbmVudHMvaG9tZS9yZWdpc3Rlci5jb21wb25lbnQuY3NzIiwic291cmNlc0NvbnRlbnQiOlsiYm9keSB7XHJcbn1cclxuIl19 */"

/***/ }),

/***/ "./src/app/components/home/register.component.html":
/*!*********************************************************!*\
  !*** ./src/app/components/home/register.component.html ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<form class=\"animated fadeInDown\">\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"lastName\">Last Name</label>\r\n      <input type=\"text\" style=\"width:200px\" name=\"lastName\" class=\"form-control\" id=\"lastName\" placeholder=\"Last Name\" [(ngModel)]=\"person.lastName\">\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"firstName\">First Name</label>\r\n      <input type=\"text\" style=\"width:200px\" name=\"firstName\" class=\"form-control\" id=\"firstName\" placeholder=\"First Name\" [(ngModel)]=\"person.firstName\">\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"email\">Email</label>\r\n      <input type=\"email\" style=\"width:200px\" name=\"email\" class=\"form-control\" id=\"email\" placeholder=\"Email Address\" [(ngModel)]=\"person.email\">\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"phone\">Phone</label>\r\n      <input type=\"tel\" style=\"width:200px\" name=\"phone\" class=\"form-control\" id=\"phone\" placeholder=\"Telephone Number\" [(ngModel)]=\"person.phone\">\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"dob\">Date of Birth</label>\r\n      <input type=\"date\" style=\"width:200px\" name=\"birthday\" class=\"form-control\" id=\"dob\" placeholder=\"Date of Birth\" [(ngModel)]=\"person.birthday\">\r\n    </div>\r\n  </div>\r\n  <div class=\"form-row\">\r\n    <div class=\"form-group col-md-6\">\r\n      <label for=\"role\">Register As</label>\r\n      <select style=\"width:150px\" name=\"role\" class=\"form-control\" [(ngModel)]=\"person.role\">\r\n        <option *ngFor=\"let role of roles\" [ngValue]=\"role\">{{role}}</option>\r\n      </select>\r\n    </div>\r\n  </div>\r\n  <input class=\"btn btn-success\" style=\"cursor:pointer\" (click)=\"RegisterPerson()\" value=\"Register!\" [hidden]=\"!connected\">\r\n</form>\r\n"

/***/ }),

/***/ "./src/app/components/home/register.component.ts":
/*!*******************************************************!*\
  !*** ./src/app/components/home/register.component.ts ***!
  \*******************************************************/
/*! exports provided: RegisterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RegisterComponent", function() { return RegisterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_home_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/home.service */ "./src/app/services/home.service.ts");
/* harmony import */ var _services_signalr_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/signalr.service */ "./src/app/services/signalr.service.ts");
/* harmony import */ var _models_Person_Model__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../models/Person.Model */ "./src/app/models/Person.Model.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ngx-toastr */ "./node_modules/ngx-toastr/fesm5/ngx-toastr.js");
/* harmony import */ var uuid__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! uuid */ "./node_modules/uuid/index.js");
/* harmony import */ var uuid__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(uuid__WEBPACK_IMPORTED_MODULE_6__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var RegisterComponent = /** @class */ (function () {
    function RegisterComponent(homeService, signalRService, router, toastr) {
        this.homeService = homeService;
        this.signalRService = signalRService;
        this.router = router;
        this.toastr = toastr;
        this.person = new _models_Person_Model__WEBPACK_IMPORTED_MODULE_3__["Person"]();
        this.roles = ['Owner', 'Tenant'];
    }
    RegisterComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscribeToEvents();
        this.signalRService.connectionEstablished.subscribe(function (state) {
            _this.connected = state;
        });
    };
    RegisterComponent.prototype.subscribeToEvents = function () {
        var _this = this;
        this.signalRService.personCreated.subscribe(function (event) {
            console.log(event);
            if (event.Success) {
                var payload = JSON.parse(event.Payload);
                _this.router.navigateByUrl('/account', { state: { email: payload.email, role: payload.role } });
            }
            else {
                _this.toastr.error(event.Error, 'Failed Registration', { timeOut: 15000, positionClass: 'toast-top-center' });
            }
        });
    };
    RegisterComponent.prototype.RegisterPerson = function () {
        var _this = this;
        this.person.cmd = this.signalRService.GetCommander();
        if (this.person.cmd) {
            if (this.person.birthday) {
                if (this.person.email) {
                    if (this.person.firstName) {
                        if (this.person.lastName) {
                            if (this.person.phone) {
                                if (this.person.role) {
                                    var data = { Commander: this.person.cmd, Command: "CreateAccount", CommandId: Object(uuid__WEBPACK_IMPORTED_MODULE_6__["v4"])(), Payload: JSON.stringify(this.person) };
                                    this.homeService.createPerson(JSON.stringify(data))
                                        .subscribe(function (data) {
                                        _this.person = new _models_Person_Model__WEBPACK_IMPORTED_MODULE_3__["Person"]();
                                        _this.toastr.info(data, 'Hi!', { timeOut: 15000, positionClass: 'toast-top-center' });
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
        console.log(JSON.stringify(this.person));
    };
    RegisterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'register-div',
            template: __webpack_require__(/*! ./register.component.html */ "./src/app/components/home/register.component.html"),
            providers: [_services_home_service__WEBPACK_IMPORTED_MODULE_1__["HomeService"], _services_signalr_service__WEBPACK_IMPORTED_MODULE_2__["SignalRService"]],
            styles: [__webpack_require__(/*! ./register.component.css */ "./src/app/components/home/register.component.css")]
        }),
        __metadata("design:paramtypes", [_services_home_service__WEBPACK_IMPORTED_MODULE_1__["HomeService"], _services_signalr_service__WEBPACK_IMPORTED_MODULE_2__["SignalRService"], _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"], ngx_toastr__WEBPACK_IMPORTED_MODULE_5__["ToastrService"]])
    ], RegisterComponent);
    return RegisterComponent;
}());



/***/ }),

/***/ "./src/app/models/Account.Model.ts":
/*!*****************************************!*\
  !*** ./src/app/models/Account.Model.ts ***!
  \*****************************************/
/*! exports provided: Account */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Account", function() { return Account; });
var Account = /** @class */ (function () {
    function Account() {
        this.cmd = ''; //hack
        this.email = '';
        this.password = '';
        this.confirm = '';
        this.role = '';
    }
    return Account;
}());



/***/ }),

/***/ "./src/app/models/Event.ts":
/*!*********************************!*\
  !*** ./src/app/models/Event.ts ***!
  \*********************************/
/*! exports provided: PushEvent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PushEvent", function() { return PushEvent; });
var PushEvent = /** @class */ (function () {
    function PushEvent() {
    }
    return PushEvent;
}());



/***/ }),

/***/ "./src/app/models/Person.Model.ts":
/*!****************************************!*\
  !*** ./src/app/models/Person.Model.ts ***!
  \****************************************/
/*! exports provided: Person */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Person", function() { return Person; });
var Person = /** @class */ (function () {
    function Person() {
        this.cmd = ''; //hack
        this.email = '';
        this.phone = '';
        this.lastName = '';
        this.firstName = '';
        this.role = '';
        this.birthday = '';
    }
    return Person;
}());



/***/ }),

/***/ "./src/app/providers/errorhandler.ts":
/*!*******************************************!*\
  !*** ./src/app/providers/errorhandler.ts ***!
  \*******************************************/
/*! exports provided: HostelErrorHandler */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HostelErrorHandler", function() { return HostelErrorHandler; });
var HostelErrorHandler = /** @class */ (function () {
    function HostelErrorHandler() {
    }
    HostelErrorHandler.prototype.handleError = function (error) {
        // console.error(Object.getOwnPropertyNames(error))
        // Object.getOwnPropertyNames(error).forEach(p => console.error(error[p]))
        console.error(error.fileName, error.lineNumber, ':', error.columnNumber, '\n', error.message, error.rejection);
    };
    return HostelErrorHandler;
}());



/***/ }),

/***/ "./src/app/providers/httpinterceptor.ts":
/*!**********************************************!*\
  !*** ./src/app/providers/httpinterceptor.ts ***!
  \**********************************************/
/*! exports provided: HostelInterceptor */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HostelInterceptor", function() { return HostelInterceptor; });
var HostelInterceptor = /** @class */ (function () {
    function HostelInterceptor() {
    }
    HostelInterceptor.prototype.intercept = function (req, next) {
        if (!req.headers.has('Content-Type')) {
            req = req.clone({
                headers: req.headers.set('Content-Type', 'application/json')
            });
        }
        req = req.clone({ headers: req.headers.set('Accept', 'application/json') });
        return next.handle(req);
    };
    return HostelInterceptor;
}());



/***/ }),

/***/ "./src/app/services/home.service.ts":
/*!******************************************!*\
  !*** ./src/app/services/home.service.ts ***!
  \******************************************/
/*! exports provided: HomeService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HomeService", function() { return HomeService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _shared_app_constants__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../shared/app.constants */ "./src/app/shared/app.constants.ts");
/* harmony import */ var uuid__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! uuid */ "./node_modules/uuid/index.js");
/* harmony import */ var uuid__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(uuid__WEBPACK_IMPORTED_MODULE_5__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var HomeService = /** @class */ (function () {
    function HomeService(http) {
        this.http = http;
        this.personActionUrl = _shared_app_constants__WEBPACK_IMPORTED_MODULE_4__["CONFIGURATION"].baseUrls.api + 'person';
        this.accountActionUrl = _shared_app_constants__WEBPACK_IMPORTED_MODULE_4__["CONFIGURATION"].baseUrls.api + 'account';
    }
    HomeService.prototype.createPerson = function (person) {
        return this.http.post(this.personActionUrl, person)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["catchError"])(this.handleError));
    };
    HomeService.prototype.createAccount = function (account) {
        var data = { Commander: account.cmd, Command: "CreateAccount", CommandId: Object(uuid__WEBPACK_IMPORTED_MODULE_5__["v4"])(), Payload: account };
        var jsonData = JSON.stringify(data);
        return this.http
            .post(this.accountActionUrl, jsonData)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["catchError"])(this.handleError));
    };
    HomeService.prototype.handleError = function (error) {
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])(error || 'Server error');
    };
    HomeService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"]])
    ], HomeService);
    return HomeService;
}());



/***/ }),

/***/ "./src/app/services/signalr.service.ts":
/*!*********************************************!*\
  !*** ./src/app/services/signalr.service.ts ***!
  \*********************************************/
/*! exports provided: SignalRService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SignalRService", function() { return SignalRService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _aspnet_signalr__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @aspnet/signalr */ "./node_modules/@aspnet/signalr/dist/esm/index.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var _shared_app_constants__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../shared/app.constants */ "./src/app/shared/app.constants.ts");
/* harmony import */ var uuid__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! uuid */ "./node_modules/uuid/index.js");
/* harmony import */ var uuid__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(uuid__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _models_Event__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../models/Event */ "./src/app/models/Event.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS = 2000;
var commander = Object(uuid__WEBPACK_IMPORTED_MODULE_4__["v4"])();
var SignalRService = /** @class */ (function () {
    function SignalRService() {
        this.personCreated = new rxjs__WEBPACK_IMPORTED_MODULE_2__["Subject"]();
        this.accountCreated = new rxjs__WEBPACK_IMPORTED_MODULE_2__["Subject"]();
        this.connectionEstablished = new rxjs__WEBPACK_IMPORTED_MODULE_2__["Subject"]();
        this.createConnection();
        this.registerOnServerEvents();
        this.startConnection();
        //this.commander = uuid();
    }
    SignalRService.prototype.createConnection = function () {
        this.hubConnection = new _aspnet_signalr__WEBPACK_IMPORTED_MODULE_1__["HubConnectionBuilder"]()
            .withUrl(_shared_app_constants__WEBPACK_IMPORTED_MODULE_3__["CONFIGURATION"].baseUrls.events + "home?commander=" + commander, _aspnet_signalr__WEBPACK_IMPORTED_MODULE_1__["HttpTransportType"].WebSockets)
            .configureLogging(_aspnet_signalr__WEBPACK_IMPORTED_MODULE_1__["LogLevel"].Debug)
            .build();
    };
    SignalRService.prototype.startConnection = function () {
        var _this = this;
        setTimeout(function () {
            _this.hubConnection.onclose(function () { return _this.connectionEstablished.next(false); });
            _this.hubConnection.start().then(function () {
                console.log('Hub connection started');
                _this.connectionEstablished.next(true);
            });
        }, WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS);
    };
    SignalRService.prototype.registerOnServerEvents = function () {
        var _this = this;
        this.hubConnection.on('personcreated', function (event) {
            var json = JSON.parse(event);
            var evnt = new _models_Event__WEBPACK_IMPORTED_MODULE_5__["PushEvent"]();
            evnt.Success = json.Success;
            evnt.Id = json.Id;
            evnt.Error = json.Error;
            evnt.Payload = json.Payload;
            _this.personCreated.next(evnt);
        });
        this.hubConnection.on('accountcreated', function (event) {
            _this.accountCreated.next(event);
        });
    };
    SignalRService.prototype.GetCommander = function () {
        return commander;
    };
    SignalRService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [])
    ], SignalRService);
    return SignalRService;
}());



/***/ }),

/***/ "./src/app/shared/app.constants.ts":
/*!*****************************************!*\
  !*** ./src/app/shared/app.constants.ts ***!
  \*****************************************/
/*! exports provided: CONFIGURATION */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CONFIGURATION", function() { return CONFIGURATION; });
var CONFIGURATION = {
    baseUrls: {
        events: 'https://events.hostel.com/',
        api: 'https://hostel.com/api/'
    }
};


/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
var environment = {
    production: false
};


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! exports provided: getBaseUrl */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getBaseUrl", function() { return getBaseUrl; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
var providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];
if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])(providers).bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.log(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! D:\a\DDD\Hostel\Presentation\Web\Landing\Hostel.Web.Landing.Angular\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map