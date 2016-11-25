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
var Item_1 = require('./Item');
var app_service_1 = require('./../app.service');
var product_1 = require('./../class/product');
var ProductComponent = (function () {
    function ProductComponent(service, route) {
        this.service = service;
        this.route = route;
        this.productName = 'Product #1';
        this.productId = '12345678';
        this.price = "100€";
        this.category = 'Computers';
        this.description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod\n  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim\n  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea\n  commodo consequat. Duis aute irure dolor in reprehenderit in voluptate\n  velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint\n  occaecat cupidatat non proident, sunt in culpa qui officia deserunt\n  mollit anim id est laborum.";
        this.images = ["https://www.google.com", "image2", "image3", "image4"];
        this.features = ["Feature #1", "Feature #2", "Feature #3", "Feature #4"];
        this.item0 = new Item_1.Item("item0");
        this.item1 = new Item_1.Item("item1");
        this.item2 = new Item_1.Item("item2");
        this.item3 = new Item_1.Item("item3");
        this.item4 = new Item_1.Item("item4");
        this.item5 = new Item_1.Item("item5");
        this.categories = [];
        this.history = [];
        this.search = [];
        this.product = {};
        this.hint = '';
        this.item0.addItem(this.item1);
        this.item0.addItem(this.item2);
        this.item1.addItem(this.item3);
        this.item1.addItem(this.item4);
        this.item3.addItem(this.item5);
    }
    ProductComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.getCategories();
        this.route.params.subscribe(function (params) {
            _this.id = params['id'];
            _this.getProduct();
            _this.getHistory();
        });
    };
    ProductComponent.prototype.eventHandler = function (event) {
        var _this = this;
        //console.log(event, event.keyCode, event.keyIdentifier);
        this.service.getCustomerByName(this.hint + event.key)
            .subscribe(function (suggestions) { return _this.search = suggestions; }, function (error) { return _this.errorMessage = error; });
    };
    ProductComponent.prototype.getHistory = function () {
        var _this = this;
        this.service.getSalesHistoryByProduct(this.id, '30')
            .subscribe(function (records) { return _this.history = records; }, function (error) { return _this.errorMessage = error; });
    };
    ProductComponent.prototype.getCategories = function () {
        var _this = this;
        this.service.getCategoriesList()
            .subscribe(function (categories) { return _this.categories = categories; }, function (error) { return _this.errorMessage = error; });
    };
    ProductComponent.prototype.getProduct = function () {
        var _this = this;
        this.service.getProduct(this.id)
            .subscribe(function (product) { _this.product = new product_1.Product(product); }, function (error) { return _this.errorMessage = error; });
    };
    ProductComponent = __decorate([
        core_1.Component({
            selector: 'product',
            moduleId: module.id,
            templateUrl: 'index.html',
            styleUrls: ['style.css'],
            providers: [app_service_1.Service],
            inputs: ['hint']
        }), 
        __metadata('design:paramtypes', [app_service_1.Service, router_1.ActivatedRoute])
    ], ProductComponent);
    return ProductComponent;
}());
exports.ProductComponent = ProductComponent;
//# sourceMappingURL=component.js.map