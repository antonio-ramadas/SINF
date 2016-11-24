"use strict";
var Customer = (function () {
    function Customer(data) {
        this.phone = data['phoneNumber'];
        this.name = data['name'];
        this.id = data['id'];
        this.email = data['email'];
        this.address = data['address'];
        this.salesPersonName = data['salesPersonName'];
        this.salesPersonId = data['salesPersonId'];
        this.nationality = data['nationality'];
        this.dateOfBirth = data['dateOfBirth'];
        this.gender = data['gender'];
        this.nif = data['nif'];
    }
    return Customer;
}());
exports.Customer = Customer;
//# sourceMappingURL=customer.js.map