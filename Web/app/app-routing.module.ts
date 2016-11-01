import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ClientComponent }      from './client/component';
import { ManagerComponent }     from './dashboard/manager/component';
import { SalesRepComponent }    from './dashboard/sales-rep/component';
import { LoginComponent }       from './login/component';
import { ProductComponent }     from './product/component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'client/:id',  component: ClientComponent },
  { path: 'dashboard/manager', component: ManagerComponent },
  { path: 'dashboard/sales-rep', component: SalesRepComponent },
  { path: 'login', component: LoginComponent },
  { path: 'product', component: ProductComponent },
  { path: '**', redirectTo: '/login'}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})

export class AppRoutingModule {}
