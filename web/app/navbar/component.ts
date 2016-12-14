import { Component } from '@angular/core';
import myGlobals = require('./../globals');

@Component({
  selector: 'navbar',
  styleUrls: [ 'app/navbar/style.css' ],
  templateUrl: 'app/navbar/index.html'
})

export class NavbarComponent {
  id = myGlobals.id;
}