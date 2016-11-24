"use strict";
var Customer = (function () {
    function Customer(data) {
        this.phone = data['phoneNumber'];
        this.name = data['name'];
    }
    return Customer;
}());
exports.Customer = Customer;
//# sourceMappingURL=customer.js.map