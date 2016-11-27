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
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var forms_2 = require("@angular/forms");
var app_routing_module_1 = require("./app-routing.module");
var ng2_bootstrap_1 = require("ng2-bootstrap/ng2-bootstrap");
var ng2_bootstrap_2 = require("ng2-bootstrap/ng2-bootstrap");
var ng2_bootstrap_3 = require("ng2-bootstrap/ng2-bootstrap");
var tree_view_1 = require("./product/tree-view");
var ng2_charts_1 = require("ng2-charts/ng2-charts");
var app_component_1 = require("./app.component");
var component_1 = require("./client/component");
var component_2 = require("./dashboard/manager/component");
var component_3 = require("./dashboard/sales-rep/component");
var component_4 = require("./login/component");
var component_5 = require("./product/component");
var component_6 = require("./navbar/component");
var search_component_1 = require("./search/search.component");
var salesorder_component_1 = require("./salesorder/salesorder.component");
var graph_test_1 = require("./dashboard/sales-rep/graph-test");
var graph_test2_1 = require("./dashboard/sales-rep/graph-test2");
var graph_test3_1 = require("./dashboard/sales-rep/graph-test3");
var map_test_1 = require("./dashboard/sales-rep/map-test");
var core_2 = require("angular2-google-maps/core");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            forms_1.FormsModule,
            forms_2.ReactiveFormsModule,
            ng2_charts_1.ChartsModule,
            platform_browser_1.BrowserModule,
            ng2_bootstrap_3.ModalModule,
            app_routing_module_1.AppRoutingModule,
            ng2_bootstrap_1.DropdownModule,
            ng2_bootstrap_2.PaginationModule,
            core_2.AgmCoreModule.forRoot({
                apiKey: 'AIzaSyAGYHY_VL3DJcBenEANyhtGs2iJd6Strpk'
            })
        ],
        declarations: [
            app_component_1.AppComponent,
            search_component_1.SearchComponent,
            salesorder_component_1.SalesOrderComponent,
            component_4.LoginComponent,
            component_1.ClientComponent,
            component_2.ManagerComponent,
            component_3.SalesRepComponent,
            component_5.ProductComponent,
            tree_view_1.TreeViewComponent,
            component_6.NavbarComponent,
            graph_test_1.DoughnutChartDemoComponent,
            graph_test2_1.LineChartDemoComponent,
            graph_test3_1.BarChartDemoComponent,
            map_test_1.MapTest
        ],
        bootstrap: [app_component_1.AppComponent]
    }),
    __metadata("design:paramtypes", [])
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map