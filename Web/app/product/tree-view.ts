import { Component, Input} from '@angular/core';
import { Item } from './Item';

@Component({
  selector: 'tree-view',
  template: `
  <ul class="catalogue-list">
    <li *ngFor="let item of root.children">
      {{item.name}}
      <span *ngIf="item.children.length != 0">
        <tree-view [root]="item"> </tree-view>
      </span>
    </li>
  </ul>
  `
})
export class TreeViewComponent{
  @Input()
  root: Item;
}
