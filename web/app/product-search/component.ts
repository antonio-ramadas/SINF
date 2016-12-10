import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Service } from './../app.service';
import { Product } from './../class/product';

@Component({
  selector: 'product-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service],
  inputs: ['hint']
})

export class ProductSearchComponent {
  list = [];
  hint = '';

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 6;

  public pageChanged(event:any):void {
    this.productsToDisplay();
  };

  id: string;
  errorMessage: string;
  products = [];
  productsTotal = [];
  categories = [];

  eventHandler(event) {
   //console.log(event, event.keyCode, event.keyIdentifier);
   //console.log(this.hint);
  }

  constructor(private route: ActivatedRoute, private service: Service) {
  }

  ngOnInit(): void {
    this.getCategories();
    this.route.params.subscribe((params : Params) => {
      this.id = params['id'];
      this.getProducts();
    });
  }

  getCategories() {
    this.service.getCategoriesList()
                    .subscribe(
                       categories => this.categories = categories,
                       error =>  this.errorMessage = <any>error);
  }

  getProducts() {
    this.service.getProductsList()
                .subscribe(
                       prod => {this.productsTotal = []; for (let obj of prod) this.productsTotal.push(new Product(obj)); this.productsToDisplay();},
                       error =>  this.errorMessage = <any>error);
  }

  productsToDisplay() {
    this.list = [];
    for (var i = 0; i < this.productsTotal.length; i++) {
      this.list.push(i.toString());
    }
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.products = this.productsTotal.slice(start, start+this.itemsPerPage);
  }
}
