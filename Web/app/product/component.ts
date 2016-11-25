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
  productName = 'Product #1';
  productId = '12345678';
  price = "100€";
  category = 'Computers';
  description = `Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim
  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea
  commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
  velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
  occaecat cupidatat non proident, sunt in culpa qui officia deserunt
  mollit anim id est laborum.`;

  images = ["https://www.google.com","image2","image3","image4"];

  features = ["Feature #1","Feature #2","Feature #3","Feature #4"];

  orderHistoryList: Customer[];
  item0 = new Item("item0");
  item1 = new Item("item1");
  item2 = new Item("item2");
  item3 = new Item("item3");
  item4 = new Item("item4");
  item5 = new Item("item5");
  id : string;
  
  categories = [];
  history = [];
  search = [];
  product = {};
  hint = '';
  errorMessage: string;

  constructor(private service: Service, private route: ActivatedRoute){
    this.item0.addItem(this.item1);
    this.item0.addItem(this.item2);
    this.item1.addItem(this.item3);
    this.item1.addItem(this.item4);
    this.item3.addItem(this.item5);
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
