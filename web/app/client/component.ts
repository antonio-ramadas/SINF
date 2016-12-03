import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Service } from './../app.service';

@Component({
  moduleId: module.id,
  selector: 'client',
  styleUrls: ['style.css'],
  templateUrl: 'index.html',
  providers: [Service]
})

export class ClientComponent implements OnInit {
  clientId: number;
  public singleModel: string = '1';
  registerForm: FormGroup;
  id : string;
  errorMessage: string;
  customer = {};
  customers = [];
  hint = '';

  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder, private service: Service) {

  }

  eventHandler(event) {
   //console.log(event, event.keyCode, event.keyIdentifier);
   //console.log(this.hint);
   this.service.getCustomerByName(this.hint)
                    .subscribe(
                       suggestions => this.customers = suggestions,
                       error =>  this.errorMessage = <any>error);
  }

  getCostumer() {
    this.service.getCostumer(this.id)
                    .subscribe(
                       customer => this.customer = customer,
                       error =>  this.errorMessage = <any>error);
  }

  getCostumers() {
    this.service.getCostumers()
                    .subscribe(
                       customers => this.customers = customers,
                       error =>  this.errorMessage = <any>error);
  }

  ngOnInit(): void {
    this.route.params.subscribe((params : Params) => {
      this.id = params['id'];
      this.getCostumer();
      this.getCostumers();
    });
    this.route.params.forEach((params: Params) => {
      this.clientId = +params['id'];
    });
    // Create Form
    this.registerForm = this.formBuilder.group({
      firstname: '',
      lastname: '',
      address: this.formBuilder.group({
        street: '',
        zip: '',
        city: ''
      }),
      phoneNumber: '',
      email: '',
      nif: '',
      nationality: ''
    });
  }

  @ViewChild('childModal') public childModal:ModalDirective;

  public showChildModal():void {
    this.childModal.show();
  }

  public hideChildModal():void {
    this.childModal.hide();
  }

  public createNewClient():void {

    console.log("leeel");
  }

}
