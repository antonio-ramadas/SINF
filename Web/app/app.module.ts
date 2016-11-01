import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent }         from './app.component';
import { ClientComponent }      from './client/component';
import { ManagerComponent }     from './dashboard/manager/component';
import { SalesRepComponent }    from './dashboard/sales-rep/component';
import { LoginComponent }       from './login/component';
import { ProductComponent }     from './product/component';

@NgModule({
  imports:      [
                  BrowserModule,
                  AppRoutingModule
                ],
  declarations: [
                  AppComponent,
                  LoginComponent,
                  ClientComponent,
                  ManagerComponent,
                  SalesRepComponent,
                  ProductComponent
                ],
  bootstrap:    [ AppComponent ]
})

export class AppModule { }
