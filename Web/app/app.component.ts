import { Component, OnInit, ViewContainerRef } from '@angular/core';

@Component({
  selector: 'app',
  template: `
  <navbar *ngIf="showNavbar"></navbar>
  <router-outlet></router-outlet>`
})

export class AppComponent implements OnInit {
  showNavbar: boolean;

  ngOnInit(): void {
    var regex = /^\/(login)?$/g
    this.showNavbar = !regex.test(window.location.pathname);
  }

  private viewContainerRef: ViewContainerRef;

  public constructor(viewContainerRef:ViewContainerRef) {
    // You need this small hack in order to catch application root view container ref
    this.viewContainerRef = viewContainerRef;
  }

 }
