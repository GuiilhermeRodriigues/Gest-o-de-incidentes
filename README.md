# 🛡️ Plataforma de Sustentação e Gestão de Incidentes

Uma plataforma profissional para **gestão de incidentes de TI**, simulando um ambiente real de suporte nível 2/3 (N2/N3). O sistema permite que analistas gerenciem chamados, categorizem problemas, acompanhem o SLA de resolução e utilizem IA generativa como apoio técnico no diagnóstico.

## 🚀 Tecnologias Utilizadas

O projeto adota os padrões arquiteturais de **Clean Architecture** para o Backend e um SPA moderno para o Frontend.

### **Backend**
- **C# / .NET 9** (ASP.NET Core Web API)
- **Entity Framework Core** (Banco de dados em memória para testes)
- **Padrões de Projeto:** Repository Pattern e Clean Architecture (Domain, Application, Infrastructure, API)
- **Integração Simulada de IA** para troubleshooting de problemas

### **Frontend**
- **Angular 18+** (Standalone Components)
- **TypeScript**
- **CSS Customizado** com design moderno (Glassmorphism + Dark Mode)

---

## 🛠️ Como rodar o projeto localmente

Você precisará ter instalado na sua máquina o **[.NET 9 SDK](https://dotnet.microsoft.com/download)** e o **[Node.js](https://nodejs.org/)** (com o npm).

### 1. Iniciar o Backend (API)
Abra o terminal na pasta raiz do projeto e execute:

```bash
# Entre na pasta da API
cd SupportOps.Api

# Inicie o servidor
dotnet run
```
A API estará rodando localmente (normalmente em `http://localhost:5049`). O banco de dados em memória é populado automaticamente com dados de exemplo (Aplicações, Usuários) sempre que o servidor for iniciado.

### 2. Iniciar o Frontend (Angular)
Abra **outro** terminal na pasta raiz do projeto e execute:

```bash
# Entre na pasta do frontend
cd support-ops-ui

# Instale as dependências (necessário apenas na primeira vez)
npm install

# Inicie a aplicação Angular
npm start
```
O painel administrativo estará acessível no seu navegador em `http://localhost:4200`.

---

## ✨ Principais Funcionalidades

- **Dashboard Analítico:** Visão geral rápida de métricas, incidentes críticos, total de chamados em aberto e resolvidos.
- **Criação de Chamados:** Formulário reativo e dinâmico que carrega informações de aplicações e usuários da base de dados.
- **Ciclo de Vida / Máquina de Estados:** Acompanhamento realista do status de um incidente (Aberto ➔ Em Análise ➔ Em Correção ➔ Resolvido).
- **Assistente de IA Integrado:** Ferramenta na tela de detalhes do incidente que analisa o problema reportado e fornece passos sugeridos de investigação (troubleshooting) para o analista responsável.

---

> Desenvolvido com dedicação para simular operações modernas de TI. 💻☕