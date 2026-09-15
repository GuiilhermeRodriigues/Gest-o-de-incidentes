import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { IncidentService } from '../../core/services/incident.service';

@Component({
  selector: 'app-incident-create',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './incident-create.component.html'
})
export class IncidentCreateComponent implements OnInit {
  incidentService = inject(IncidentService);
  router = inject(Router);

  applications: any[] = [];
  users: any[] = [];

  formData = {
    title: '',
    description: '',
    applicationId: '',
    requesterId: '',
    category: 0,
    impact: 1,
    urgency: 1
  };

  ngOnInit() {
    this.incidentService.getApplications().subscribe(data => this.applications = data);
    this.incidentService.getUsers().subscribe(data => this.users = data);
  }

  onSubmit() {
    this.incidentService.createIncident(this.formData).subscribe({
      next: (res: any) => {
        this.router.navigate(['/incidents']);
      },
      error: (err: any) => {
        console.error('Erro ao criar incidente', err);
        alert('Erro ao criar incidente. Verifique o console.');
      }
    });
  }
}
