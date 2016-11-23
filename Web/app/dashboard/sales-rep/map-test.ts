import { Component } from '@angular/core';

@Component({
  selector: 'map',
	styles: [`
		.sebm-google-map-container {
			height: 300px;
		}
	`],
  template: `
		<sebm-google-map [latitude]="lat" [longitude]="lng"></sebm-google-map>
	`
})

export class MapTest {
  lat: number = 51.678418;
  lng: number = 7.809007;
}
