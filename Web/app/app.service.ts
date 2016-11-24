import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Product } from './class/product';
import { Customer } from './class/customer';
import { Observable }     from 'rxjs/Observable';
import 'rxjs/add/operator/catch';

@Injectable()
export class Service {
  private baseUrl = 'http://localhost:49822/api';
  private customerUrl = '/customer';  // URL to web API
  private headers = new Headers({ 'Access-Control-Allow-Origin': '*' });
  private options = new RequestOptions({ headers: this.headers });

  constructor (private http: Http) {}
  getCustomers (): Observable<JSON[]> {
    return this.http.get(this.baseUrl + this.customerUrl)
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