import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import myGlobals = require('./../globals');

@Component({
  selector: 'navbar',
  styleUrls: [ 'app/navbar/style.css' ],
  templateUrl: 'app/navbar/index.html'
})

export class NavbarComponent {
  public static idCustomer = myGlobals.idCustomer;
  public static isManager = myGlobals.manager;

  constructor(private router: Router) {

  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  get customerId() {
    return NavbarComponent.idCustomer;
  }

  get manager() {
    return NavbarComponent.isManager;
  }

  public static updateCustomer() {
    NavbarComponent.idCustomer = myGlobals.idCustomer;
  }

  public static updateType() {
    NavbarComponent.isManager = myGlobals.manager;
  }
}