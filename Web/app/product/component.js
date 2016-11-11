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
var Item_1 = require("./Item");
var ProductComponent = (function () {
    function ProductComponent() {
        this.productName = 'Product #1';
        this.productId = '12345678';
        this.price = "100€";
        this.category = 'Computers';
        this.orderHistoryList = [
            { "client": 0, "info": "Available" },
            { "client": 1, "info": "Ready" },
            { "client": 2, "info": "Started" }
        ];
        this.item0 = new Item_1.Item("item0");
        this.item1 = new Item_1.Item("item1");
        this.item2 = new Item_1.Item("item2");
        this.item3 = new Item_1.Item("item3");
        this.item4 = new Item_1.Item("item4");
        this.item5 = new Item_1.Item("item5");
        this.item0.addItem(this.item1);
        this.item0.addItem(this.item2);
        this.item1.addItem(this.item3);
        this.item1.addItem(this.item4);
        this.item3.addItem(this.item5);
    }
    return ProductComponent;
}());
ProductComponent = __decorate([
    core_1.Component({
        selector: 'product',
        moduleId: module.id,
        templateUrl: 'index.html',
        styleUrls: ['style.css']
    }),
    __metadata("design:paramtypes", [])
], ProductComponent);
exports.ProductComponent = ProductComponent;
//# sourceMappingURL=component.js.map