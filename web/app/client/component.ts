import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';
import { Service } from './../app.service';
import { SalesOrder } from './../class/salesorder';
import { Customer } from './../class/customer';

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

  editInfo() {
    this.registerForm.patchValue({'id': this.customer.id});
    this.registerForm.patchValue({'phoneNumber': this.customer.phone});
    this.registerForm.patchValue({'name': this.customer.name});
    this.registerForm.patchValue({'email': this.customer.email});
    this.registerForm.patchValue({'address': this.customer.address});
    this.registerForm.patchValue({'nationality': this.customer.nationality});
    this.registerForm.patchValue({'birthDate': this.customer.dateOfBirth});
    this.registerForm.patchValue({'gender': this.customer.gender});
    this.registerForm.patchValue({'nif': this.customer.nif});
    this.childModal.show();
  }

  updateNotes() {
    this.service.updateCustomerNotes(this.customer.id, this.customer.notes);
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCustomer();
      this.getCustomers();
      this.getSalesHistory();
    });
    // Create Form
    this.registerForm = this.formBuilder.group({
      id: '',
      name: '',
      address: '',
      phoneNumber: '',
      birthDate: '',
      email: '',
      nif: '',
      nationality: '',
      gender: '',
      labels: '',
      notes: ''
    });
  }

  @ViewChild('smModal') public childModal: ModalDirective;

  public showChildModal(): void {
    this.childModal.show();
  }

  public hideChildModal(): void {
    this.childModal.hide();
  }

  public createNewClient(): void {
    console.error("TODO submit form to create new client. Waiting for the API correct implementation");
  }

}
