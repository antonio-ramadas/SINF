export class Item {
  children: Array<Item>;
  name: string;

  constructor(n: string) {
    this.name = n;
    this.children = [];
  }

  addItem(item: Item){
    this.children.push(item);
  }
}
