"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var MapTest = (function () {
    function MapTest() {
        this.lat = 51.678418;
        this.lng = 7.809007;
    }
    MapTest = __decorate([
        core_1.Component({
            selector: 'map',
            styles: ["\n\t\t.sebm-google-map-container {\n\t\t\theight: 300px;\n\t\t}\n\t"],
            template: "\n\t\t<sebm-google-map [latitude]=\"lat\" [longitude]=\"lng\"></sebm-google-map>\n\t"
        }), 
        __metadata('design:paramtypes', [])
    ], MapTest);
    return MapTest;
}());
exports.MapTest = MapTest;
//# sourceMappingURL=map-test.js.map