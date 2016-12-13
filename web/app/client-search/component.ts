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
  groups = ['Subgroup #1','Subgroup #2','Subgroup #3'];

  customers = [];
  customersSearch = [];
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
   this.hint = this.hint.toLowerCase();
   this.searchCustomer();
  }

  public pageChanged(event:any):void {
    //console.log('Page changed to: ' + event.page);
    //console.log('Number items per page: ' + event.itemsPerPage);
    this.currentPage = event.page;
    this.customersToDisplay();
  };

  redirect(path: string) {
    this.router.navigate([path]);
  }

  constructor(private route: ActivatedRoute, private router: Router, private service: Service) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCustomers();
    });
  }

  searchCustomer() {
    this.customers = [];
    this.customersSearch = [];

    for (let c of this.customersTotal)
      if (c.isSimilar(this.hint))
        this.customersSearch.push(c);

    let start = (this.currentPage-1)*this.itemsPerPage;
    this.customers = this.customersSearch.slice(start, start+this.itemsPerPage);
  }

  getCustomers() {
    this.service.getCostumers()
      .subscribe(
      customers => { this.customersTotal = []; for (let customer of customers) this.customersTotal.push(new Customer(customer)); this.customersToDisplay();},
      error => this.errorMessage = <any>error);
  }

  customersToDisplay() {
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.customers = this.customersTotal.slice(start, start+this.itemsPerPage);
  }
}
