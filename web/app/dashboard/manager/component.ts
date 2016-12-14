import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../../app.service';

@Component({
  selector: 'manager',
  styleUrls: [ 'app/dashboard/manager/styles.css' ],
  templateUrl: 'app/dashboard/manager/index.html',
  providers: [Service]
})

export class ManagerComponent {
  id: string;

  constructor(private route: ActivatedRoute, private service: Service) {
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
  }
}