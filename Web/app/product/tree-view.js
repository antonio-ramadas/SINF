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
var Item_1 = require('./Item');
var TreeViewComponent = (function () {
    function TreeViewComponent() {
    }
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Item_1.Item)
    ], TreeViewComponent.prototype, "root", void 0);
    TreeViewComponent = __decorate([
        core_1.Component({
            selector: 'tree-view',
            template: "\n  <ul class=\"catalogue-list\">\n    <li *ngFor=\"let item of root.children\">\n      <a> {{item.name}} </a>\n      <span *ngIf=\"item.children.length != 0\">\n        <tree-view [root]=\"item\"> </tree-view>\n      </span>\n    </li>\n  </ul>\n  ",
            styles: ["\n  .catalogue-list{\n    list-style: none;\n    padding: 1rem;\n  }\n  "]
        }), 
        __metadata('design:paramtypes', [])
    ], TreeViewComponent);
    return TreeViewComponent;
}());
exports.TreeViewComponent = TreeViewComponent;
//# sourceMappingURL=tree-view.js.map