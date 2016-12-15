import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../../app.service';

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
  tasks: { title: string, date: string }[] = [
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" },
    { title: "Task Title", date: "11:23 4/7/2017" }
  ]
  clients: { name: string, info: string }[] = [
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
    { name: "Client Name", info: "Client information" },
  ]

  constructor(private route: ActivatedRoute, private service: Service) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
    this.directionsService = new google.maps.DirectionsService();
    this.directionsDisplay = new google.maps.DirectionsRenderer();
		navigator.geolocation.getCurrentPosition(this.updateCoord);
    obj = this;
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
      origin: coord,
      destination: "Valongo",
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
