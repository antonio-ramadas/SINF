import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app',
  template: '<navbar *ngIf="showNavbar"></navbar><router-outlet></router-outlet>'
})

export class AppComponent implements OnInit {
  showNavbar: boolean;

  ngOnInit(): void {
    var regex = /^\/login$/g
    this.showNavbar = !regex.test(window.location.pathname);
  }
 }