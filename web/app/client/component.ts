import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';
import { Service } from './../app.service';
import { SalesOrder } from './../class/salesorder';

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
  id: string;
  errorMessage: string;
  customer = {};
  customers = [];
  salesHistory = [];
  hint = '';

  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder, private service: Service, private location: Location) {

  }

  eventHandler(event) {
    //console.log(event, event.keyCode, event.keyIdentifier);
    //console.log(this.hint);
    if (this.hint.length <= 0)
      this.getCostumers();
    else
      this.service.getCustomerByName(this.hint)
        .subscribe(
        suggestions => this.customers = suggestions,
        error => this.errorMessage = <any>error);
  }

  changeCustomer(newId) {
    this.id = newId;
    this.getCostumer();
    this.getSalesHistory();
    this.location.replaceState("/client/" + newId);
  }

  getSalesHistory() {
    this.service.getSalesHistoryByCustomer(this.id, '30')
      .subscribe(
      sales => { this.salesHistory = []; for (let sale of sales) this.salesHistory.push(new SalesOrder(sale)); },
      error => this.errorMessage = <any>error);
  }

  getCostumer() {
    this.service.getCostumer(this.id)
      .subscribe(
      customer => this.customer = customer,
      error => this.errorMessage = <any>error);
  }

  getCostumers() {
    this.service.getCostumers()
      .subscribe(
      customers => this.customers = customers,
      error => this.errorMessage = <any>error);
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCostumer();
      this.getCostumers();
      this.getSalesHistory();
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

  @ViewChild('childModal') public childModal: ModalDirective;

  public showChildModal(): void {
    this.childModal.show();
  }

  public hideChildModal(): void {
    this.childModal.hide();
  }

  public createNewClient(): void {

    console.log("leeel");
  }

}
