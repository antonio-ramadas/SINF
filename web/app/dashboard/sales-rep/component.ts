import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../../app.service';

@Component({
  moduleId: module.id,
  selector: 'sales-rep',
  styleUrls: ['style.css'],
  templateUrl: 'index.html',
  providers: [Service]
})

export class SalesRepComponent {
  id: string;

  constructor(private route: ActivatedRoute, private service: Service) {
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
  }
}
