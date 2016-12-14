import { NgModule, ApplicationRef, Injectable }       from '@angular/core';
import { BrowserModule }                  from '@angular/platform-browser';
import { AppRoutingModule }               from './app-routing.module';
import { DropdownModule }                 from 'ng2-bootstrap/ng2-bootstrap';
import { TreeViewComponent }              from './product/tree-view';
import { ChartsModule }                   from 'ng2-charts/ng2-charts';
import { HttpModule, JsonpModule }        from '@angular/http';
import { ToastyModule }                     from 'ng2-toasty';

import { AppComponent }                   from './app.component';
import { ClientComponent }                from './client/component';
import { ManagerComponent }               from './dashboard/manager/component';
import { SalesRepComponent }              from './dashboard/sales-rep/component';
import { LoginComponent }                 from './login/component';
import { SalesOrderComponent }            from './salesorder/component';
import { CartComponent }                  from './cart/component';
import { ProductComponent }               from './product/component';
import { NavbarComponent }                from './navbar/component';
import { SearchComponent }                from './search/search.component';
import { DoughnutChartDemoComponent }     from './dashboard/sales-rep/graph-test';
import { LineChartDemoComponent }         from './dashboard/sales-rep/graph-test2';
import { BarChartDemoComponent }          from './dashboard/sales-rep/graph-test3';
import { MapTest }                        from './dashboard/sales-rep/map-test';
import { AgmCoreModule }                  from 'angular2-google-maps/core';
import { FormsModule }                    from '@angular/forms';
import { ReactiveFormsModule }            from '@angular/forms';
import { ModalModule }                    from 'ng2-bootstrap/ng2-bootstrap';
import { PaginationModule }               from 'ng2-bootstrap/ng2-bootstrap';
import { ProductSearchComponent }         from './product-search/component';
import { ClientSearchComponent }          from './client-search/component';
import { RepSearchComponent }             from './rep-search/component';

@NgModule({
  imports:      [
                  FormsModule,
                  ReactiveFormsModule,
                  ChartsModule,
                  BrowserModule,
                  ModalModule,
                  AppRoutingModule,
                  DropdownModule,
                  PaginationModule,
                  AgmCoreModule.forRoot({
                      apiKey: 'AIzaSyAGYHY_VL3DJcBenEANyhtGs2iJd6Strpk'
                    }),
                  HttpModule,
                  JsonpModule,
                  FormsModule,
                  ToastyModule.forRoot()
                ],
  declarations: [
                  AppComponent,
                  SearchComponent,
                  ProductSearchComponent,
                  ClientSearchComponent,
                  RepSearchComponent,
                  LoginComponent,
                  SalesOrderComponent,
                  CartComponent,
                  ClientComponent,
                  ManagerComponent,
                  SalesRepComponent,
                  ProductComponent,
                  TreeViewComponent,
                  NavbarComponent,
                  DoughnutChartDemoComponent,
                  LineChartDemoComponent,
                  BarChartDemoComponent,
                  MapTest
                ],
  bootstrap:    [ AppComponent ]
})

export class AppModule { }
