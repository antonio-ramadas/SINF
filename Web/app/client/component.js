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
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var forms_1 = require("@angular/forms");
var ClientComponent = (function () {
    function ClientComponent(route, formBuilder) {
        this.route = route;
        this.formBuilder = formBuilder;
        this.singleModel = '1';
    }
    ClientComponent.prototype.ngOnInit = function () {
        var _this = this;
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
    return ClientComponent;
}());
__decorate([
    core_1.ViewChild('childModal'),
    __metadata("design:type", ng2_bootstrap_1.ModalDirective)
], ClientComponent.prototype, "childModal", void 0);
ClientComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'client',
        styleUrls: ['style.css'],
        templateUrl: 'index.html'
    }),
    __metadata("design:paramtypes", [router_1.ActivatedRoute, forms_1.FormBuilder])
], ClientComponent);
exports.ClientComponent = ClientComponent;
//# sourceMappingURL=component.js.map