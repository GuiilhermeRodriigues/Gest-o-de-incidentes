import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { IncidentListComponent } from './features/incidents/incident-list.component';
import { IncidentCreateComponent } from './features/incidents/incident-create.component';
import { IncidentDetailsComponent } from './features/incidents/incident-details.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'incidents', component: IncidentListComponent },
  { path: 'incidents/new', component: IncidentCreateComponent },
  { path: 'incidents/:id', component: IncidentDetailsComponent },
  { path: '**', redirectTo: '' }
];
