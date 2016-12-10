import { Component, Input } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Item } from './Item';
import { Service } from './../app.service';
import { Customer } from './../class/customer';
import { Product } from './../class/product';
import { SalesOrder } from './../class/salesorder';

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
  product = {'imageUrl': 'default.jpg'};
  hint = '';
  total = '';
  errorMessage: string;

  constructor(private service: Service, private router: Router, private route: ActivatedRoute){
  }

  ngOnInit() {
    this.getCategories();
    this.route.params.subscribe((params : Params) => {
      this.id = params['id'];
      this.getProduct();
      this.getHistory();
    });
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
