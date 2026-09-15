import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { IncidentService } from '../../core/services/incident.service';
import { IncidentResponse, IncidentStatus, IncidentPriority } from '../../core/models/incident.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-incident-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './incident-details.component.html'
})
export class IncidentDetailsComponent implements OnInit {
  route = inject(ActivatedRoute);
  incidentService = inject(IncidentService);
  http = inject(HttpClient);

  incident: IncidentResponse | null = null;
  aiAnalysis: any = null;
  isAnalyzing = false;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.incidentService.getIncident(id).subscribe({
        next: (data) => this.incident = data,
        error: (err) => console.error(err)
      });
    }
  }

  analyzeIncident(): void {
    if (!this.incident) return;
    this.isAnalyzing = true;
    this.http.post(`http://localhost:5049/api/incidents/${this.incident.id}/analyze`, {}).subscribe({
      next: (result) => {
        this.aiAnalysis = result;
        this.isAnalyzing = false;
      },
      error: (err) => {
        console.error(err);
        this.isAnalyzing = false;
      }
    });
  }

  resolveIncident(): void {
    if (!this.incident) return;
    
    // According to the state machine, we must go to EM_ANALISE then EM_CORRECAO then RESOLVIDO
    // We'll simulate this by firing the three requests sequentially to jump the states automatically
    this.incidentService.changeStatus(this.incident.id, 1).subscribe({
      next: () => {
        this.incidentService.changeStatus(this.incident!.id, 4).subscribe({
          next: () => {
            this.incidentService.changeStatus(this.incident!.id, 5).subscribe({
              next: () => {
                if(this.incident) this.incident.status = 5;
                alert('Incidente resolvido com sucesso!');
              }
            });
          }
        });
      },
      error: (err) => {
        console.error(err);
        alert('Erro ao resolver incidente.');
      }
    });
  }

  getPriorityLabel(priority: IncidentPriority): string {
    return ['Baixa', 'Média', 'Alta', 'Crítica'][priority];
  }

  getStatusLabel(status: IncidentStatus): string {
    return ['Aberto', 'Em Análise', 'Aguardando Usuário', 'Aguardando Terceiro', 'Em Correção', 'Resolvido', 'Encerrado', 'Cancelado'][status];
  }
}
