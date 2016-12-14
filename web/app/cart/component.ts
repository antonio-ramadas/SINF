import { Component, ViewChild, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { Service } from './../app.service';
import { Customer } from './../class/customer';
import { Cart } from './../class/cart';
import { CartLine } from './../class/cartline';

@Component({
  selector: 'wishlist',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service]
})

export class CartComponent {
  id: string;
  carts: CartLine[] = [];
  customer: Customer = new Customer(JSON.parse("{}"));
  total: number = 0;
  errorMessage: string;
  checkout: FormGroup;
  @ViewChild('childModal') public childModal:ModalDirective;

  // Create Form
  ngOnInit(): void {
    this.checkout = this.formBuilder.group({
        street: '',
        fiscalstreet: ''
    });
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getWishlist();
      this.getCustomer();
    });
  }

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private service: Service) {

  }

  removeLine(productId: string) {
    console.log("TODO API call");
    for (let i = 0; i < this.carts.length; i++) {
      if (productId == this.carts[i].productID) {
        this.carts.splice(i, 1);
      }
    }
  }

  updateTotal() {
    this.total = 0;
    for (let cart of this.carts) {
      if (cart.active) {
        this.total += (Number(cart.sellingPrice) * Number(cart.quantity));
      }
    }
  }

  getCustomer() {
    this.service.getCustomer(this.id)
        .subscribe(
        client => {this.customer = new Customer(client); this.checkout.patchValue({'street': this.customer.address}); this.checkout.patchValue({'fiscalstreet': this.customer.address});},
        error => this.errorMessage = <any>error);
  }

  getWishlist() {
    this.service.getWishlist(this.id)
        .subscribe(
        wishs => {this.carts = []; for (let wish of wishs) this.addItem(new CartLine(wish));},
        error => this.errorMessage = <any>error);
  }

  addItem(c: CartLine) {
    for (let cart of this.carts) {
      if (cart.productID == c.productID) {
        cart.quantity += Number(c.quantity);
        return;
      }
    }

    this.carts.push(c);
  }

  public showChildModal():void {
    this.childModal.show();
  }

  public hideChildModal():void {
    this.childModal.hide();
  }
}
