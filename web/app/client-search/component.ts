import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Service } from './../app.service';
import { Customer } from './../class/customer';

@Component({
  selector: 'client-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service],
  inputs: ['hint']
})

export class ClientSearchComponent {
  list = ['1','2','3','4','5','6','7'];
  groups = ['Subgroup #1','Subgroup #2','Subgroup #3'];

  customers = [];
  customersTotal = [];
  id: string;
  errorMessage: string;
  hint = '';

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 7;

  eventHandler(event) {
   //console.log(event, event.keyCode, event.keyIdentifier);
   //console.log(this.hint);
  }

  public pageChanged(event:any):void {
    //console.log('Page changed to: ' + event.page);
    //console.log('Number items per page: ' + event.itemsPerPage);
    this.currentPage = event.page;
    this.customersToDisplay();
  };

  constructor(private route: ActivatedRoute, private service: Service) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCustomers();
    });
  }

  getCustomers() {
    this.service.getCostumers()
      .subscribe(
      customers => { this.customersTotal = []; for (let customer of customers) this.customersTotal.push(new Customer(customer)); this.customersToDisplay();},
      error => this.errorMessage = <any>error);
  }

  customersToDisplay() {
    this.list = [];
    for (var i = 0; i < this.customersTotal.length; i++) {
      this.list.push(i.toString());
    }
    console.log(this.currentPage);
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.customers = this.customersTotal.slice(start, start+this.itemsPerPage);
  }
}
