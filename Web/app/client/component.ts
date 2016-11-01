import { Component, OnInit }              from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'client',
  styleUrls: [ 'app/client/style.css' ],
  templateUrl: 'app/client/index.html'
})

export class ClientComponent implements OnInit {
  clientId: number;

  constructor(
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.clientId = +params['id'];
    });
  }
}