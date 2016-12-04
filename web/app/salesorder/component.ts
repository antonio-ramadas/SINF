import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../app.service';

@Component({
  selector: 'salesorder',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css'],
  providers: [Service]
})

export class SalesOrderComponent implements OnInit {
  id: string;

  constructor(private route: ActivatedRoute, private service: Service) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
  }

}
