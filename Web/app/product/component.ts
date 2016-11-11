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
  orderHistoryList: { client: number, info: string }[] = [
    { "client": 0, "info": "Available" },
    { "client": 1, "info": "Ready" },
    { "client": 2, "info": "Started" }
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
