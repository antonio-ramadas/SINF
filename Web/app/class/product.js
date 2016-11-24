"use strict";
var Product = (function () {
    function Product(product) {
        this.id = product['id'];
        this.price = product['price'];
        this.description = product['description'];
        this.salesCount = product['salesCount'];
        this.stock = product['quantity'];
        this.imageUrls = product['imageUrls'];
        this.family = product['family'];
        this.vat = product['vat'];
        this.warehouses = product['warehouses'];
    }
    return Product;
}());
exports.Product = Product;
//# sourceMappingURL=product.js.map