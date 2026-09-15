import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { IncidentService } from '../../core/services/incident.service';
import { IncidentResponse, IncidentStatus, IncidentPriority } from '../../core/models/incident.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  incidentService = inject(IncidentService);
  
  incidents: IncidentResponse[] = [];
  openCount = 0;
  resolvedCount = 0;
  criticalCount = 0;

  ngOnInit(): void {
    this.incidentService.getIncidents().subscribe({
      next: (data) => {
        this.incidents = data;
        this.openCount = data.filter(i => i.status === IncidentStatus.ABERTO || i.status === IncidentStatus.EM_ANALISE).length;
        this.resolvedCount = data.filter(i => i.status === IncidentStatus.RESOLVIDO).length;
        this.criticalCount = data.filter(i => i.priority === IncidentPriority.CRITICA).length;
      },
      error: (err) => console.error('Erro ao carregar incidentes', err)
    });
  }

  getPriorityLabel(priority: IncidentPriority): string {
    const labels = ['Baixa', 'Média', 'Alta', 'Crítica'];
    return labels[priority];
  }

  getPriorityClass(priority: IncidentPriority): string {
    const classes = ['badge-info', 'badge-info', 'badge-warning', 'badge-danger'];
    return classes[priority];
  }

  getStatusLabel(status: IncidentStatus): string {
    return ['Aberto', 'Em Análise', 'Aguardando Usuário', 'Aguardando Terceiro', 'Em Correção', 'Resolvido', 'Encerrado', 'Cancelado'][status];
  }
}
