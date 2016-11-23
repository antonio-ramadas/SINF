"use strict";
var Item = (function () {
    function Item(n) {
        this.name = n;
        this.children = [];
    }
    Item.prototype.addItem = function (item) {
        this.children.push(item);
    };
    return Item;
}());
exports.Item = Item;
//# sourceMappingURL=Item.js.map