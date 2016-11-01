import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app',
  template: '<navbar *ngIf="showNavbar"></navbar><router-outlet></router-outlet>'
})

export class AppComponent implements OnInit {
  showNavbar: boolean;

  ngOnInit(): void {
    var regex = /\/(client\/\d+|dashboard\/manager|dashboard\/sales-rep|product)/g
    this.showNavbar = regex.test(window.location.pathname);
  }
 }