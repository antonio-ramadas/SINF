import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Service } from './../app.service';
import { NavbarComponent } from './../navbar/component';
import myGlobals = require('./../globals');

@Component({
  selector: 'login',
  styleUrls: [ 'app/login/style.css' ],
  templateUrl: 'app/login/index.html',
  providers: [Service],
  inputs: ['user', 'pass']
})

export class LoginComponent {
  user: string = "";
  pass: string = "";

  constructor(private router: Router, private route: ActivatedRoute, private service: Service) {

  }

  submit() {
    let path = '/dashboard/';

    if (this.user == "1") {
      path += 'manager/';
      myGlobals.manager = true;
      NavbarComponent.updateType();
    } else {
      path += 'sales-rep/';
      myGlobals.manager = false;
      NavbarComponent.updateType();
    }
      myGlobals.idSales = this.user;

      myGlobals.idCustomer = "";
      NavbarComponent.updateCustomer();

      this.redirect(path + myGlobals.idSales);
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

}