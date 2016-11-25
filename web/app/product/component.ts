import { Component, Input } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Item } from './Item';
import { Service } from './../app.service';
import { Customer } from './../class/customer';
import { Product } from './../class/product';

@Component({
  selector: 'product',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service],
  inputs: ['hint']
})

export class ProductComponent {
  orderHistoryList: Customer[];
  id : string;
  categories = [];
  history = [];
  search = [];
  product = {};
  hint = '';
  errorMessage: string;
  images = ["https://www.google.com","image2","image3","image4"];

  constructor(private service: Service, private route: ActivatedRoute){
  }

  ngOnInit() {
    this.getCategories();
    this.route.params.subscribe((params : Params) => {
      this.id = params['id'];
      this.getProduct();
      this.getHistory();
    });
  }

  eventHandler(event) {
   //console.log(event, event.keyCode, event.keyIdentifier);
   this.service.getCustomerByName(this.hint + event.key)
                    .subscribe(
                       suggestions => this.search = suggestions,
                       error =>  this.errorMessage = <any>error);
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
}
