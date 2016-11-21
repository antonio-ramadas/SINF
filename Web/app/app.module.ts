import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent }         from './app.component';
import { ClientComponent }      from './client/component';
import { ManagerComponent }     from './dashboard/manager/component';
import { SalesRepComponent }    from './dashboard/sales-rep/component';
import { LoginComponent }       from './login/component';
import { ProductComponent }     from './product/component';
import { NavbarComponent }      from  './navbar/component';
import { DropdownModule }       from  'ng2-bootstrap/ng2-bootstrap';

@NgModule({
  imports:      [
                  BrowserModule,
                  AppRoutingModule,
                  DropdownModule
                ],
  declarations: [
                  AppComponent,
                  LoginComponent,
                  ClientComponent,
                  ManagerComponent,
                  SalesRepComponent,
                  ProductComponent,
                  NavbarComponent
                ],
  bootstrap:    [ AppComponent ]
})

export class AppModule { }
