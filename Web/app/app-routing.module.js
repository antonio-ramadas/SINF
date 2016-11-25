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
var router_1 = require('@angular/router');
var component_1 = require('./client/component');
var component_2 = require('./dashboard/manager/component');
var component_3 = require('./dashboard/sales-rep/component');
var component_4 = require('./login/component');
var component_5 = require('./product/component');
var search_component_1 = require('./search/search.component');
var routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'client/:id', component: component_1.ClientComponent },
    { path: 'dashboard/manager', component: component_2.ManagerComponent },
    { path: 'dashboard/sales-rep', component: component_3.SalesRepComponent },
    { path: 'login', component: component_4.LoginComponent },
    { path: 'product', component: component_5.ProductComponent },
    { path: 'search', component: search_component_1.SearchComponent },
    { path: '**', redirectTo: '/login' }
];
var AppRoutingModule = (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes)],
            exports: [router_1.RouterModule]
        }), 
        __metadata('design:paramtypes', [])
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
//# sourceMappingURL=app-routing.module.js.map