import { Component } from '@angular/core';

@Component({
  selector: 'client-search',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css']
})

export class ClientSearchComponent {
  list = ['1','2','3','4','5','6','7'];
  clients: { name: string, info: string, label: string }[] = [
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" },
    { "name": "Pedro Romano Barbosa", "info": "Client Information", "label": "Client Label" }
  ];
  groups = ['Subgroup #1','Subgroup #2','Subgroup #3'];

  public totalItems:number = 30;
  public currentPage:number = 1;
  public itemsPerPage:number = 7;

  public pageChanged(event:any):void {
    console.log('Page changed to: ' + event.page);
    console.log('Number items per page: ' + event.itemsPerPage);
  };
}
