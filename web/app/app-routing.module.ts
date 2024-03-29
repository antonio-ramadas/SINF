import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ClientComponent }      from './client/component';
import { ManagerComponent }     from './dashboard/manager/component';
import { SalesRepComponent }    from './dashboard/sales-rep/component';
import { LoginComponent }       from './login/component';
import { ProductComponent }     from './product/component';
import { SearchComponent } from './search/search.component';
import { ProductSearchComponent } from './product-search/component';
import { ClientSearchComponent } from './client-search/component';
import { RepSearchComponent }     from './rep-search/component';
import { SalesOrderComponent } from './salesorder/component';
import { CartComponent } from './cart/component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'client/:id',  component: ClientComponent },
  { path: 'dashboard/manager/:id', component: ManagerComponent },
  { path: 'dashboard/sales-rep/:id', component: SalesRepComponent },
  { path: 'login', component: LoginComponent },
  { path: 'product/:id', component: ProductComponent },
  { path: 'search', component: SearchComponent },
  { path: 'salesorder/:id', component: SalesOrderComponent },
  { path: 'salesorder', component: SalesOrderComponent },
  { path: 'cart/:id', component: CartComponent },
  { path: 'product-search', component: ProductSearchComponent },
  { path: 'client-search', component: ClientSearchComponent },
  { path: 'rep-search', component: RepSearchComponent },
  { path: '**', redirectTo: '/login'}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})

export class AppRoutingModule {}
