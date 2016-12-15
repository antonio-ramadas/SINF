import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Service } from './../../app.service';
import { Product } from './../../class/product';

@Component({
  selector: 'manager',
  styleUrls: [ 'app/dashboard/manager/styles.css' ],
  templateUrl: 'app/dashboard/manager/index.html',
  providers: [Service]
})

export class ManagerComponent {
  id: string;
  products = [];
  salesRep = [];
  errorMessage: string;

  constructor(private router: Router, private route: ActivatedRoute, private service: Service) {
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getTopSalesRep();
      this.getTopProducts();
    });
  }

  getTopSalesRep() {
    this.service.getTopSalesRep('30')
          .subscribe(
          salesRep => this.salesRep = salesRep,
          error => this.errorMessage = <any>error);
  }

  getTopProducts() {
    this.service.getTopProducts('30')
          .subscribe(
          products => this.products = products,
          error => this.errorMessage = <any>error);
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }
}