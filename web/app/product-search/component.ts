import { Component } from '@angular/core';

@Component({
  selector: 'product-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css']
})

export class ProductSearchComponent {
  list = ['1','2','3','4','5','6','7'];
  products: { src: string, name: string, info: string }[] = [
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
    { "src": "img", "name": "Product Name", "info": "Information" },
];

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 7;

  public pageChanged(event:any):void {
    console.log('Page changed to: ' + event.page);
    console.log('Number items per page: ' + event.itemsPerPage);
  };
}
