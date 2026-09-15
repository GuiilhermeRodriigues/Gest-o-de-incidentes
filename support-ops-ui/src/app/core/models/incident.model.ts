export enum IncidentCategory {
  SOFTWARE = 0,
  HARDWARE = 1,
  REDE = 2,
  ACESSO = 3,
  OUTROS = 4
}

export enum IncidentPriority {
  BAIXA = 0,
  MEDIA = 1,
  ALTA = 2,
  CRITICA = 3
}

export enum IncidentStatus {
  ABERTO = 0,
  EM_ANALISE = 1,
  AGUARDANDO_USUARIO = 2,
  AGUARDANDO_TERCEIRO = 3,
  EM_CORRECAO = 4,
  RESOLVIDO = 5,
  ENCERRADO = 6,
  CANCELADO = 7
}

export enum SlaStatus {
  OK = 0,
  WARNING = 1,
  VIOLATED = 2
}

export interface IncidentResponse {
  id: string;
  number: string;
  title: string;
  description: string;
  applicationId: string;
  applicationName: string;
  requesterId: string;
  requesterName: string;
  assigneeId?: string;
  assigneeName?: string;
  category: IncidentCategory;
  priority: IncidentPriority;
  status: IncidentStatus;
  openedAt: string;
  slaDeadline: string;
  slaStatus: SlaStatus;
}
