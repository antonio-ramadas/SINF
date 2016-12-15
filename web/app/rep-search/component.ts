import { Component } from '@angular/core';

@Component({
  selector: 'rep-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css']
})

export class RepSearchComponent {
  list = ['1','2','3','4','5','6','7'];
  reps: { name: string, info: string }[] = [
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
    { "name": "Pedro Romano Barbosa", "info": "Rep Information" },
  ];

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 7;

  public pageChanged(event:any):void {
    console.log('Page changed to: ' + event.page);
    console.log('Number items per page: ' + event.itemsPerPage);
  };
}
