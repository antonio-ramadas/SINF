import { Injectable } from '@angular/core';
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

  constructor (private http: Http) {}
  getCustomers (): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.customerPath)
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