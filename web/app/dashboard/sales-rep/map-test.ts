import { Component, OnInit} from '@angular/core';

declare var google: any;

var obj;

@Component({
  selector: 'map',
	styles: [`
		.sebm-google-map-container {
			height: 300px;
		}
	`],
  template: `
		<!--<sebm-google-map [latitude]="lat" [zoom]="zoom" [longitude]="lng"></sebm-google-map>-->
		<div id="map"></div>
	`
})

export class MapTest {
	zoom: number = 1;

	ngOnInit(){
		obj = this;
		var lat;
		var lng;
		navigator.geolocation.getCurrentPosition(this.updateCoord);
		var map = new google.maps.Map(document.getElementById('map'), {
          center: {lat: -34.397, lng: 150.644},
          zoom: 8
        });
  }

	updateCoord(geo) {
		obj.lat = geo.coords.latitude;
		obj.lng = geo.coords.longitude;
		obj.zoom = 15;
	}

}
