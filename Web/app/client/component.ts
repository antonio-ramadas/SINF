import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ButtonsModule } from 'ng2-bootstrap/ng2-bootstrap';

@Component({
  moduleId: module.id,
  selector: 'client',
  styleUrls: ['style.css'],
  templateUrl: 'index.html'
})

export class ClientComponent implements OnInit {
  clientId: number;
  public singleModel: string = '1';

  constructor(
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.clientId = +params['id'];
    });
  }
}
