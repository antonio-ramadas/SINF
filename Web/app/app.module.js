"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var platform_browser_1 = require('@angular/platform-browser');
var app_routing_module_1 = require('./app-routing.module');
var app_component_1 = require('./app.component');
var component_1 = require('./client/component');
var component_2 = require('./dashboard/manager/component');
var component_3 = require('./dashboard/sales-rep/component');
var component_4 = require('./login/component');
var component_5 = require('./product/component');
var component_6 = require('./navbar/component');
var ng2_bootstrap_1 = require('ng2-bootstrap/ng2-bootstrap');
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                app_routing_module_1.AppRoutingModule,
                ng2_bootstrap_1.DropdownModule
            ],
            declarations: [
                app_component_1.AppComponent,
                component_4.LoginComponent,
                component_1.ClientComponent,
                component_2.ManagerComponent,
                component_3.SalesRepComponent,
                component_5.ProductComponent,
                component_6.NavbarComponent
            ],
            bootstrap: [app_component_1.AppComponent]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map