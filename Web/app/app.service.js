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
var http_1 = require('@angular/http');
var Observable_1 = require('rxjs/Observable');
require('rxjs/add/operator/catch');
var Service = (function () {
    function Service(http) {
        this.http = http;
        this.baseUrl = 'http://localhost:49822/api';
        this.customerPath = '/customer'; // Path to web API
        this.productPath = '/product';
        this.categoryPath = '/category';
        this.historyPath = '/sales/history';
        this.searchPath = '/search';
    }
    Service.prototype.getSalesHistoryByProduct = function (id, total) {
        return this.http.get(this.baseUrl + this.historyPath + '/' + id + '/' + total)
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.getCustomers = function () {
        return this.http.get(this.baseUrl + this.customerPath)
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.getCustomerByName = function (hint) {
        return this.http.get(this.baseUrl + this.customerPath + this.searchPath + '/' + hint)
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.getProduct = function (id) {
        /*let params: URLSearchParams = new URLSearchParams();
        params.set('id', id);*/
        return this.http.get(this.baseUrl + this.productPath + '/' + id) //, { search: params } )
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.getProductsList = function () {
        return this.http.get(this.baseUrl + this.productPath)
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.getCategoriesList = function () {
        return this.http.get(this.baseUrl + this.categoryPath)
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.getProductListByCategory = function (id) {
        var params = new http_1.URLSearchParams();
        params.set('categoryId', id);
        return this.http.get(this.baseUrl + this.categoryPath, { search: params })
            .map(this.extractData)
            .catch(this.handleError);
    };
    Service.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    Service.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
        var errMsg;
        if (error instanceof http_1.Response) {
            var body = error.json() || '';
            var err = body.error || JSON.stringify(body);
            errMsg = error.status + " - " + (error.statusText || '') + " " + err;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable_1.Observable.throw(errMsg);
    };
    Service = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], Service);
    return Service;
}());
exports.Service = Service;
//# sourceMappingURL=app.service.js.map