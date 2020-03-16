import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers/auth.guard';
import { AddExpenseComponent } from './addexpense';
import { GetExpensesComponent } from './getexpenses';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'addexpense', component: AddExpenseComponent, canActivate: [AuthGuard]},
  {path: 'getexpenses', component: GetExpensesComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
