import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { IncidentService } from '../../core/services/incident.service';
import { IncidentResponse, IncidentPriority, IncidentStatus } from '../../core/models/incident.model';

@Component({
  selector: 'app-incident-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './incident-list.component.html'
})
export class IncidentListComponent implements OnInit {
  incidentService = inject(IncidentService);
  incidents: IncidentResponse[] = [];
  filteredIncidents: IncidentResponse[] = [];
  searchTerm = '';

  ngOnInit(): void {
    this.incidentService.getIncidents().subscribe({
      next: (data) => {
        this.incidents = data;
        this.filteredIncidents = data;
      },
      error: (err) => console.error(err)
    });
  }

  onSearch(event: any): void {
    this.searchTerm = event.target.value.toLowerCase();
    this.filteredIncidents = this.incidents.filter(i => 
      i.title.toLowerCase().includes(this.searchTerm) || 
      i.number.toLowerCase().includes(this.searchTerm)
    );
  }

  getPriorityLabel(priority: IncidentPriority): string {
    return ['Baixa', 'Média', 'Alta', 'Crítica'][priority];
  }
  
  getPriorityClass(priority: IncidentPriority): string {
    return ['badge-info', 'badge-info', 'badge-warning', 'badge-danger'][priority];
  }

  getStatusLabel(status: IncidentStatus): string {
    return ['Aberto', 'Em Análise', 'Aguardando Usuário', 'Aguardando Terceiro', 'Em Correção', 'Resolvido', 'Encerrado', 'Cancelado'][status];
  }
}
