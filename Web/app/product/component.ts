import { Component } from '@angular/core';
import { Item } from './Item';

@Component({
  selector: 'product',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css']
})

export class ProductComponent {
  productName = 'Product #1';
  productId = '12345678';
  price = "100€";
  category = 'Computers';
  description = `Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
  tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim
  veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea
  commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
  velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
  occaecat cupidatat non proident, sunt in culpa qui officia deserunt
  mollit anim id est laborum.`;

  images = ["https://www.google.com","image2","image3","image4"];

  features = ["Feature #1","Feature #2","Feature #3","Feature #4"];

  orderHistoryList: { client: number, info: string }[] = [
    { "client": 1214, "info": "Pedro Romano Barbosa" },
    { "client": 424242, "info": "António Ramadas" },
    { "client": 123, "info": "Duarte Pinto" }
  ];
  item0 = new Item("item0");
  item1 = new Item("item1");
  item2 = new Item("item2");
  item3 = new Item("item3");
  item4 = new Item("item4");
  item5 = new Item("item5");

  constructor(){
    this.item0.addItem(this.item1);
    this.item0.addItem(this.item2);
    this.item1.addItem(this.item3);
    this.item1.addItem(this.item4);
    this.item3.addItem(this.item5);
  }
}
