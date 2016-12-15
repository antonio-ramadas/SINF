import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Service } from './../app.service';
import { SalesOrder } from './../class/salesorder';
import { Customer } from './../class/customer';
import { SalesRepresentative } from './../class/salesrep';

@Component({
  selector: 'salesorder',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service]
})

export class SalesOrderComponent implements OnInit {
  id: string;
  errorMessage: string;
  salesOrder: SalesOrder = new SalesOrder(JSON.parse("{}"));
  customer: Customer = new Customer(JSON.parse("{}"));
  salesRep: SalesRepresentative = new SalesRepresentative(JSON.parse("{}"));

  constructor(private route: ActivatedRoute, private router: Router, private service: Service) {

  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getSalesOrder();
    });
  }

  getSalesOrder() {
    this.service.getSalesOrder(this.id)
        .subscribe(
        sale => {this.salesOrder = new SalesOrder(sale); this.getMoreInfo();},
        error => this.errorMessage = <any>error);
  }

  getMoreInfo() {
    this.service.getCustomer(this.salesOrder.entity)
        .subscribe(
        sale => this.customer = new Customer(sale),
        error => this.errorMessage = <any>error);
        
    this.service.getSalesRepresentativeById(this.salesOrder.salesRep)
        .subscribe(
        sale => this.salesRep = new SalesRepresentative(sale),
        error => this.errorMessage = <any>error);
  }
}
