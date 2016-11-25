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
var ng2_bootstrap_1 = require('ng2-bootstrap/ng2-bootstrap');
var forms_1 = require('@angular/forms');
var app_service_1 = require('./../app.service');
var ClientComponent = (function () {
    function ClientComponent(route, formBuilder, service) {
        this.route = route;
        this.formBuilder = formBuilder;
        this.service = service;
        this.singleModel = '1';
        this.customer = {};
        this.customers = [];
    }
    ClientComponent.prototype.getCostumer = function () {
        var _this = this;
        this.service.getCostumer(this.id)
            .subscribe(function (customer) { return _this.customer = customer; }, function (error) { return _this.errorMessage = error; });
    };
    ClientComponent.prototype.getCostumers = function () {
        var _this = this;
        this.service.getCostumers()
            .subscribe(function (customers) { return _this.customers = customers; }, function (error) { return _this.errorMessage = error; });
    };
    ClientComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.id = params['id'];
            _this.getCostumer();
            _this.getCostumers();
        });
        this.route.params.forEach(function (params) {
            _this.clientId = +params['id'];
        });
        // Create Form
        this.registerForm = this.formBuilder.group({
            firstname: '',
            lastname: '',
            address: this.formBuilder.group({
                street: '',
                zip: '',
                city: ''
            }),
            phoneNumber: '',
            email: '',
            nif: '',
            nationality: ''
        });
    };
    ClientComponent.prototype.showChildModal = function () {
        this.childModal.show();
    };
    ClientComponent.prototype.hideChildModal = function () {
        this.childModal.hide();
    };
    ClientComponent.prototype.createNewClient = function () {
        console.log("leeel");
    };
    __decorate([
        core_1.ViewChild('childModal'), 
        __metadata('design:type', ng2_bootstrap_1.ModalDirective)
    ], ClientComponent.prototype, "childModal", void 0);
    ClientComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'client',
            styleUrls: ['style.css'],
            templateUrl: 'index.html',
            providers: [app_service_1.Service]
        }), 
        __metadata('design:paramtypes', [router_1.ActivatedRoute, forms_1.FormBuilder, app_service_1.Service])
    ], ClientComponent);
    return ClientComponent;
}());
exports.ClientComponent = ClientComponent;
//# sourceMappingURL=component.js.map