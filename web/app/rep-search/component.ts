import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { SalesRepresentative } from './../class/salesrep';
import { Service } from './../app.service';

@Component({
  selector: 'rep-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service],
  inputs: ['hint']
})

export class RepSearchComponent {
  list = ['1','2','3','4','5','6','7'];
  reps: SalesRepresentative[] = [];
  repsTotal: SalesRepresentative[] = [];
  repsSearch: SalesRepresentative[] = [];
  errorMessage: string;
  hint = "";

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 7;

  constructor(private router: Router, private route: ActivatedRoute, private service: Service) {

  }

  ngOnInit(): void {
      this.getSalesRep();
  };

  eventHandler(event) {
   //console.log(event, event.keyCode, event.keyIdentifier);
   //console.log(this.hint);
   this.hint = this.hint.toLowerCase();
   this.searchSalesRep();
  }

  searchSalesRep() {
    this.reps = [];
    this.repsSearch = [];

    for (let r of this.repsTotal) {
      if (r.isSimilar(this.hint)) {
        this.repsSearch.push(r);
      }
    }

    this.list = [];
    for (var i = 0; i < this.repsSearch.length; i++) {
      this.list.push(i.toString());
    }
    
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.reps = this.repsSearch.slice(start, start+this.itemsPerPage);
  }

  getSalesRep() {
    this.service.getSalesRepresentatives()
      .subscribe(
        sales => {this.repsTotal = []; for (let sale of sales) this.repsTotal.push(new SalesRepresentative(sale)); this.salesRepToDisplay();},
        error => this.errorMessage = <any>error);
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  public pageChanged(event:any):void {
    console.log('Page changed to: ' + event.page);
    console.log('Number items per page: ' + event.itemsPerPage);
  };

  salesRepToDisplay() {
    this.list = [];
    for (var i = 0; i < this.repsTotal.length; i++) {
      this.list.push(i.toString());
    }
    
    let start = (this.currentPage-1)*this.itemsPerPage;
    this.reps = this.repsTotal.slice(start, start+this.itemsPerPage);
  }
}
