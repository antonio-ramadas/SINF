import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';
import { Service } from './../app.service';
import { SalesOrder } from './../class/salesorder';
import { Customer } from './../class/customer';
import myGlobals = require('./../globals');

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
  customer: Customer = new Customer(JSON.parse('{}'));
  customers = [];
  salesHistory = [];
  countries = [];
  labels = [];
  createClient: boolean = false;
  hint = '';

  constructor(private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder, private service: Service, private location: Location) {

  }

  eventHandler(event) {
    //console.log(event, event.keyCode, event.keyIdentifier);
    //console.log(this.hint);
    if (this.hint.length <= 0)
      this.getCustomers();
    else
      this.service.getCustomerByName(this.hint)
        .subscribe(
        suggestions => this.customers = suggestions,
        error => this.errorMessage = <any>error);
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  openCreateClientModal() {
    this.createClient = true;
    this.showChildModal();
  }

  goToSalesOrder(salesId) {
    this.router.navigate(["/salesorder/" + salesId]);
  }

  changeCustomer(newId) {
    this.router.navigate(["/client/" + newId]);
  }

  getSalesHistory() {
    this.service.getSalesHistoryByCustomer(this.id, '30')
      .subscribe(
      sales => { this.salesHistory = []; for (let sale of sales) this.salesHistory.push(new SalesOrder(sale)); },
      error => this.errorMessage = <any>error);
  }

  getCustomer() {
    this.service.getCustomer(this.id)
      .subscribe(
      customer => this.customer = new Customer(customer),
      error => this.errorMessage = <any>error);
  }

  getCustomers() {
    this.service.getCostumers()
      .subscribe(
      customers => { this.customers = []; for (let customer of customers) this.customers.push(new Customer(customer)); },
      error => this.errorMessage = <any>error);
  }

  getCountries() {
    this.service.getCountries()
      .subscribe(
      countries => this.countries = countries,
      error => this.errorMessage = <any>error);
  }

  getLabels() {
    this.service.getLabels()
          .subscribe(
          labels => this.labels = labels,
          error => this.errorMessage = <any>error);
  }

  editInfo() {
    this.createClient = false;
    this.registerForm.patchValue({'id': this.customer.id});
    this.registerForm.patchValue({'phoneNumber': this.customer.phone});
    this.registerForm.patchValue({'name': this.customer.name});
    this.registerForm.patchValue({'email': this.customer.email});
    this.registerForm.patchValue({'address': this.customer.address});
    this.registerForm.patchValue({'nationality': this.customer.nationality});
    this.registerForm.patchValue({'nif': this.customer.nif});
    this.registerForm.patchValue({'label1': this.customer.label1});
    this.registerForm.patchValue({'label2': this.customer.label2});
    this.registerForm.patchValue({'label3': this.customer.label3});
    this.childModal.show();
  }

  updateNotes() {
    this.service.updateCustomerNotes(this.customer.id, this.customer.notes);
  }

  signInCustomer() {
    myGlobals.idCustomer = this.id;
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCustomer();
      this.getCustomers();
      this.getSalesHistory();
      this.getCountries();
      this.getLabels();
    });
    // Create Form
    this.registerForm = this.formBuilder.group({
      id: '',
      name: '',
      address: '',
      phoneNumber: '',
      email: '',
      nif: '',
      nationality: '',
      label1: '',
      label2: '',
      label3: ''
    });
  }

  @ViewChild('smModal') public childModal: ModalDirective;

  public showChildModal(): void {
    this.childModal.show();
  }

  public hideChildModal(): void {
    this.childModal.hide();
  }

  public handleClient(): void {
    let id = this.registerForm.controls["id"].value;
    let json;
    json = {
        "id": this.registerForm.controls["id"].value,
        "name": this.registerForm.controls["name"].value,
        "address": this.registerForm.controls["address"].value,
        "email": this.registerForm.controls["email"].value,
        "phoneNumber": this.registerForm.controls["phoneNumber"].value,
        "nationality": this.registerForm.controls["nationality"].value,
        "nif": this.registerForm.controls["nif"].value,
        "labels": [
          this.registerForm.controls["label1"].value,
          this.registerForm.controls["label2"].value,
          this.registerForm.controls["label3"].value
        ]
    };

    if (this.createClient)
      this.service.createCustomer(<JSON>json);
    else
      this.service.updateCustomer(id, <JSON>json);

    this.hideChildModal();
  }

}
