import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  moduleId: module.id,
  selector: 'client',
  styleUrls: ['style.css'],
  templateUrl: 'index.html'
})

export class ClientComponent implements OnInit {
  clientId: number;
  public singleModel: string = '1';
  registerForm: FormGroup;

  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
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
