import { Routes } from '@angular/router';
import {AuthenticationComponent} from './authentication/authentication.component';
import {DashboardComponent} from './dashboard.component/dashboard.component';
import {UrlInfoComponent} from './url-info.component/url-info.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: AuthenticationComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'info/:id', component: UrlInfoComponent },

  { path: '**', redirectTo: '/login' }
];
