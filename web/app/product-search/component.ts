import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Service } from './../app.service';
import { Product } from './../class/product';
import myGlobals = require('./../globals');

@Component({
  selector: 'product-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service],
  inputs: ['hint']
})

export class ProductSearchComponent {
  hint = '';
  category = '';

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 6;
  public maxSize:number = 6;

  public pageChanged(event:any):void {
    this.currentPage = event.page;
    this.searchProduct();
  };

  id: string;
  errorMessage: string;
  products = [];
  productSearch = [];
  productsTotal = [];
  categories = [];

  eventHandler(event) {
   //console.log(event, event.keyCode, event.keyIdentifier);
   //console.log(this.hint);
   this.hint = this.hint.toLowerCase();
   this.searchProduct();
  }

  selectCategory(id: string) {
    this.category = id;
    this.searchProduct();
  }

  constructor(private router: Router, private route: ActivatedRoute, private service: Service) {
  }

  ngOnInit(): void {
    this.getCategories();
    this.route.params.subscribe((params : Params) => {
      this.id = params['id'];
      this.getProducts();
    });
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  getCategories() {
    this.service.getCategoriesList()
                    .subscribe(
                       categories => this.categories = categories,
                       error =>  this.errorMessage = <any>error);
  }

  searchProduct() {
    this.products = [];
    this.productSearch = [];

    for (let p of this.productsTotal) {
      if (p.isSimilar(this.hint, this.category)) {
        this.productSearch.push(p);
      }
    }

    this.totalItems = this.productSearch.length;
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.products = this.productSearch.slice(start, start+this.itemsPerPage);
  }

  getProducts() {
    this.service.getProductsList()
                .subscribe(
                       prod => {this.productsTotal = []; for (let obj of prod) this.productsTotal.push(new Product(obj)); this.showProducts();},
                       error =>  this.errorMessage = <any>error);
  }

  showProducts() {
    if (myGlobals.productCategory == null) {
       this.productsToDisplay();
    } else {
      this.selectCategory(myGlobals.productCategory);
      myGlobals.productCategory = null;
    }
  }

  productsToDisplay() {
    this.totalItems = this.productsTotal.length;
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.products = this.productsTotal.slice(start, start+this.itemsPerPage);
  }
}
