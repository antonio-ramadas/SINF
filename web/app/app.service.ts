import { Injectable, Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Http, Response, Headers, RequestOptions, URLSearchParams } from '@angular/http';

import { Product } from './class/product';
import { Customer } from './class/customer';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/catch';

@Injectable()
export class Service {
  private baseUrl = 'http://localhost:49822/api';
  private customerPath = '/customer';  // Path to web API
  private productPath = '/product';
  private categoryPath = '/category';
  private notesPath = '/notes';
  private historyPath = '/sales/history';
  private salesPath = '/sales';
  private searchPath = '/search';
  private statsPath = '/stats';
  private salesRepPath = '/salesrep';
  private incomePath = '/income';
  private wishlistPath = '/cart';
  private countriesPath = '/country';
  private labelsPath = '/label';
  private routePath = '/route';
  private totalPath = '/total';
  private topCustomersPath = '/topCustomers';
  private topProductsPath = '/topProducts';
  private incomePerSalesPath = '/income-per-sale';
  private topCategoriesPath = '/category-top';

  constructor (private router: Router, private http: Http) {}

  redirect(path: string) {
    this.router.navigate([path]);
  }

  addProductToCustomerCart(json: JSON) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    this.http.post(this.baseUrl + this.wishlistPath, json, {headers: headers})
                    .map(this.extractData)
                    .catch(this.handleError)
                    .subscribe();
  }

  getTopProductsBySalesRep(id: string, total: string) {
    return this.http.get(this.baseUrl + this.salesRepPath + this.topCustomersPath + '/' + id + '/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getTopCustomersBySalesRep(id: string, total: string) : Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.salesRepPath + this.topCustomersPath + '/' + id + '/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getRoutes(salesRepId: string) : Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.routePath + '/' + salesRepId)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getLabels() : Observable<string[]> {
    return this.http.get(this.baseUrl + this.customerPath + this.labelsPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }
  
  getCountries() : Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.countriesPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }
  
  getWishlist(id: string) : Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.wishlistPath + this.customerPath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }
  
  removeWish(id: string, product: string) {
    this.http.delete(this.baseUrl + this.wishlistPath + '/' + id + '/' + product)
                    .map(this.extractData)
                    .catch(this.handleError)
                    .subscribe();
  }
  
  createSalesOrder(json: JSON) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post(this.baseUrl + this.salesPath, json, {headers: headers})
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getSalesRepresentatives(): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.salesRepPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getSalesOrder (id: string): Observable<JSON> {
    return this.http.get(this.baseUrl + this.salesPath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getSalesHistoryByCustomer (id: string, total: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.salesPath + this.customerPath + '/' + id + '/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getSalesHistoryByProduct (id: string, total: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.historyPath + '/' + id + '/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  createCustomer(json: JSON) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    this.http.post(this.baseUrl + this.customerPath, json, {headers: headers})
                    .catch(this.handleError)
                    .subscribe();
  }

  updateCustomer(id: string, json: JSON) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    this.http.put(this.baseUrl + this.customerPath + '/' + id, json, {headers: headers})
                    .catch(this.handleError)
                    .subscribe();
  }

  updateCustomerNotes(id: string, text: string): void {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    this.http.put(this.baseUrl + this.customerPath + this.notesPath + '/' + id, JSON.stringify({'notes' : text}), {headers: headers})
                    .catch(this.handleError)
                    .subscribe();
  }

  getSalesRepresentativeById (id: string): Observable<JSON> {
    return this.http.get(this.baseUrl + this.salesRepPath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getCustomers (): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.customerPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getCustomerByName (hint: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.customerPath + this.searchPath + '/' + hint)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getProduct(id: string): Observable<JSON>{
    /*let params: URLSearchParams = new URLSearchParams();
    params.set('id', id);*/

    return this.http.get(this.baseUrl + this.productPath + '/' + id)//, { search: params } )
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getProductsList(): Observable<JSON[]>{
    return this.http.get(this.baseUrl + this.productPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getCategoriesList(): Observable<JSON[]>{
    return this.http.get(this.baseUrl + this.categoryPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getProductListByCategory(id: string): Observable<JSON[]>{
    let params: URLSearchParams = new URLSearchParams();
    params.set('categoryId', id);

    return this.http.get(this.baseUrl + this.categoryPath, { search: params } )
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getCustomer(id: string): Observable<JSON>{
    return this.http.get(this.baseUrl + this.customerPath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getCostumers(): Observable<JSON[]>{
    return this.http.get(this.baseUrl + this.customerPath)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomeBySalesRepresentativeManager(total: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.totalPath + this.incomePath + '/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomeBySalesRepresentative(id: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.incomePath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomePerSaleBySalesRepresentative(id: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.incomePerSalesPath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomeBySalesRepresentativeByYearManager(year: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.totalPath + this.incomePath + '/' + year)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomeBySalesRepresentativeByYear(id: string, year: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.incomePath + '/' + id + '/' + year)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomePerSaleBySalesRepresentativeByYearManager(year: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.totalPath + this.incomePerSalesPath + '/' + year)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomePerSaleBySalesRepresentativeByYear(id: string, year: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.incomePerSalesPath + '/' + id + '/' + year)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getIncomePerSaleByYear(year: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + '/year/' + year)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getTopCategoriesBySalesRepresentative(id: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + this.topCategoriesPath + '/' + id)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getTopProducts(total: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.productPath + '/top/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  getTopSalesRep(total: string): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.statsPath + '/salesRep/' + total)
                    .map(this.extractData)
                    .catch(this.handleError);
  }

  private extractData(res: Response) {
    let body = res.json();
    return body || { };
  }
  private handleError (error: Response | any) {
    // In a real world app, we might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }
}