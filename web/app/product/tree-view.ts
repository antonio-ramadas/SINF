import { Component, Input} from '@angular/core';
import { Item } from './item';

@Component({
  selector: 'tree-view',
  template: `
  <ul class="catalogue-list">
    <li>
      <a> {{root.description}} </a>
    </li>
    <li *ngFor="let item of root.children">
      <a> {{item.description}} </a>
      <span *ngIf="item.children.length != 0">
        <tree-view [root]="item"> </tree-view>
      </span>
    </li>
  </ul>
  `,
  styles: [`
  .catalogue-list{
    list-style: none;
    padding: 1rem;
  }
  `]
})
export class TreeViewComponent{
  @Input()
  public root: Item;
  
  constructor(){
    console.log(this.root);
  }
}
