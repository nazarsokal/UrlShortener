import { Routes } from '@angular/router';
import {AuthenticationComponent} from './authentication/authentication.component';
import {DashboardComponent} from './dashboard.component/dashboard.component';

export const routes: Routes = [
  // Redirect the empty path to the login page automatically
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: AuthenticationComponent },
  { path: 'dashboard', component: DashboardComponent },

  // Optional: A catch-all wildcard route for 404s
  { path: '**', redirectTo: '/login' }
];
