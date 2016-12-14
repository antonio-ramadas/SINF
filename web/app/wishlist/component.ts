import { Component, ViewChild, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ModalDirective } from 'ng2-bootstrap/ng2-bootstrap';

@Component({
  selector: 'wishlist',
  moduleId: module.id,
  templateUrl: 'index.html',
  styleUrls: ['style.css']
})

export class WishlistComponent {
  checkout: FormGroup;
  @ViewChild('childModal') public childModal:ModalDirective;

  // Create Form
  ngOnInit(): void {
    this.checkout = this.formBuilder.group({
      address: this.formBuilder.group({
        street: '',
        zip: '',
        city: ''
      }),
      fiscaladdress: this.formBuilder.group({
        fiscalstreet: '',
        fiscalzip: '',
        fiscalcity: ''
      })
    });
  }

  constructor(private formBuilder: FormBuilder) {

  }

  public showChildModal():void {
    this.childModal.show();
    console.log("fwefwe")
  }

  public hideChildModal():void {
    this.childModal.hide();
  }
}
