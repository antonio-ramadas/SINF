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
  customersSearch = [];
  categories = [];
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

  selectCategory(id: string) {
    let found = false;
    for (var i = 0; i < this.categories.length && !found; i++) {
      if (this.categories[i] == id) {
        this.categories.slice(i, 1);
        found = true;
      }
    }

    if (!found) {
      this.categories.push(id);
    }

    this.searchCustomer();
  }

  public pageChanged(event:any):void {
    //console.log('Page changed to: ' + event.page);
    //console.log('Number items per page: ' + event.itemsPerPage);
    this.currentPage = event.page;
    this.customersToDisplay();
  };

  constructor(private router: Router, private route: ActivatedRoute, private service: Service) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCustomers();
      this.getLabels();
    });
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  getLabels() {
    this.service.getLabels()
      .subscribe(
        labels => this.groups = labels,
        error => this.errorMessage = <any>error);
  }

  searchCustomer() {
    this.customers = [];
    this.customersSearch = [];

    for (let c of this.customersTotal) {
      if (c.isSimilar(this.hint, this.categories)) {
        this.customersSearch.push(c);
      }
    }

    this.totalItems = this.customersSearch.length;
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
    this.list = [];
    for (var i = 0; i < this.customersTotal.length; i++) {
      this.list.push(i.toString());
    }
    
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.customers = this.customersTotal.slice(start, start+this.itemsPerPage);
  }
}
