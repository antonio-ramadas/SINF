import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Service } from './../app.service';

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
    let json;
    json = {
      "username": this.user,
      "password": this.pass
    };
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

}