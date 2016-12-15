import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Service } from './../../app.service';
import { Customer } from './../../class/customer';

declare var google: any;

var obj;

@Component({
  moduleId: module.id,
  selector: 'sales-rep',
  styleUrls: ['style.css'],
  templateUrl: 'index.html',
  providers: [Service]
})

export class SalesRepComponent {
  id: string;
  zoom: number = 1;
  srcLat: number;
  srcLng: number;
  destLat: number;
  destLng: number;
  map: any;
  directionsService: any;
  directionsDisplay: any;
  tasks = [];
  clients: Customer[] = [];
  errorMessage: string;

  constructor(private router: Router, private route: ActivatedRoute, private service: Service) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.getCustomers();
      this.getRoutes();
    });
    this.directionsService = new google.maps.DirectionsService();
    this.directionsDisplay = new google.maps.DirectionsRenderer();
		navigator.geolocation.getCurrentPosition(this.updateCoord);
    obj = this;
  }

  getRoutes() {
    this.service.getRoutes(this.id)
      .subscribe(
        routes => this.tasks = routes,
        error => this.errorMessage = <any>error);
  }

  getCustomers() {
    this.service.getTopCustomersBySalesRep(this.id, '50')
      .subscribe(
        customers => {this.clients = []; for (let customer of customers) this.clients.push(new Customer(customer));},
        error => this.errorMessage = <any>error);
  }

  redirect(path: string) {
    this.router.navigate([path]);
  }

  updateCoord(geo) {
		obj.srcLat = geo.coords.latitude;
		obj.srcLng = geo.coords.longitude;
		obj.zoom = 15;
    obj.map = new google.maps.Map(document.getElementById('map'), { 
      center: { lat: obj.srcLat, lng: obj.srcLng },
      zoom: 15,
      scrollwheel: false, 
    });
	}

  updateRoute(coord) {
    var request = {
      origin: this.srcLat + ',' + this.srcLng,
      destination: coord,
      travelMode: 'DRIVING'
    };
    this.directionsService.route(request, function(result, status) {
      if (status == 'OK') {
        obj.directionsDisplay.setDirections(result);
      }
    });
    this.directionsDisplay.setMap(this.map);
  }
  
}
