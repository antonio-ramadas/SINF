import { Component } from '@angular/core';

@Component({
  selector: 'search',
  moduleId: module.id,
  templateUrl: 'search.component.html',
  styleUrls: ['search.component.css']
})

export class SearchComponent {
  // Deve ser um array de objetos representative/product
  list = ['1','2','3','4','5','6','7'];
  isProduct: boolean = false;

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 7;

  public pageChanged(event:any):void {
    console.log('Page changed to: ' + event.page);
    console.log('Number items per page: ' + event.itemsPerPage);
  };
}
