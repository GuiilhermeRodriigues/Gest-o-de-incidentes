import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncidentResponse } from '../models/incident.model';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {
  private http = inject(HttpClient);
  // Default API URL (assuming it runs on standard .NET port or we proxy)
  private apiUrl = 'http://localhost:5049/api/incidents'; 

  getIncidents(): Observable<IncidentResponse[]> {
    return this.http.get<IncidentResponse[]>(this.apiUrl);
  }

  getIncident(id: string): Observable<IncidentResponse> {
    return this.http.get<IncidentResponse>(`${this.apiUrl}/${id}`);
  }

  changeStatus(id: string, status: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/change-status`, status, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  getApplications(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5049/api/metadata/applications');
  }

  getUsers(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5049/api/metadata/users');
  }

  createIncident(data: any): Observable<IncidentResponse> {
    return this.http.post<IncidentResponse>(this.apiUrl, data);
  }
}
