import { Component, Input } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Item } from './Item';
import { Service } from './../app.service';
import { Customer } from './../class/customer';
import { Product } from './../class/product';
import { SalesOrder } from './../class/salesorder';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
import myGlobals = require('./../globals');

@Component({
  selector: 'product',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service],
  inputs: ['hint', 'total']
})

export class ProductComponent {
  orderHistoryList: Customer[];
  id : string;
  categories = [];
  history = [];
  search = [];
  product: Product = new Product(JSON.parse("{}"));
  hint = '';
  total = 1;
  errorMessage: string;
  toastPosition = 'top-center';

  constructor(
    private service: Service, 
    private router: Router,
    private route: ActivatedRoute,
    private toastyService:ToastyService,
    private toastyConfig: ToastyConfig) {
    this.toastyConfig.theme = 'default';
  }

  ngOnInit() {
    this.getCategories();
    this.route.params.subscribe((params : Params) => {
      this.id = params['id'];
      this.getProduct();
      this.getHistory();
    });
  }

  addToCustomerCart() {
    let json;
    json = {
      "customerID": myGlobals.idCustomer,
      "description": this.product.description,
      "salesRepID": myGlobals.idSales,
      "lines": [{
        "productID": this.product.id,
        "description": this.product.description,
        "quantity": this.total.toString(),
        "costPrice": this.product.price.toString(),
        "sellingPrice": this.product.price.toString()
      }]
    };

    this.service.addProductToCustomerCart(<JSON>json);

    var toastOptions: ToastOptions = {
        title: "Client Wishlist",
        msg: "This product was successfully added to client " + myGlobals.idCustomer,
        showClose: true,
        timeout: 3000
    };
    this.toastyService.warning(toastOptions);
  }

  redirectToProductCategory(cat: string) {
    myGlobals.productCategory = cat;
    this.router.navigate(["/product-search"]);
  }

  getHistory() {
    this.service.getSalesHistoryByProduct(this.id, '30')
                    .subscribe(
                       records => this.history = records,
                       error =>  this.errorMessage = <any>error);
  }

  getCategories() {
    this.service.getCategoriesList()
                    .subscribe(
                       categories => this.categories = categories,
                       error =>  this.errorMessage = <any>error);
  }

  getProduct() {
    this.service.getProduct(this.id)
                    .subscribe(
                       product => {this.product = new Product(product);},
                       error =>  this.errorMessage = <any>error);
  }

  goToSalesOrder(salesId) {
    this.router.navigate(["/salesorder/" + salesId]);
  }
}
