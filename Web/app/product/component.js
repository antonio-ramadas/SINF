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
        this.description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod\n  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim\n  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea\n  commodo consequat. Duis aute irure dolor in reprehenderit in voluptate\n  velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint\n  occaecat cupidatat non proident, sunt in culpa qui officia deserunt\n  mollit anim id est laborum.";
        this.images = ["https://www.google.com", "image2", "image3", "image4"];
        this.features = ["Feature #1", "Feature #2", "Feature #3", "Feature #4"];
        this.orderHistoryList = [
            { "client": 1214, "info": "Pedro Romano Barbosa" },
            { "client": 424242, "info": "António Ramadas" },
            { "client": 123, "info": "Duarte Pinto" }
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