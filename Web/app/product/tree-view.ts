import { Component, Input} from '@angular/core';
import { Item } from './Item';

@Component({
  selector: 'tree-view',
  template: `
  <ul class="catalogue-list">
    <li *ngFor="let item of root.children">
      <a> {{item.name}} </a>
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
  root: Item;
}
