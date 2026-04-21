# 💰 Budget Tracker

A full stack budgeting web application built with Vue.js and .NET 10, hosted on Microsoft Azure.

---

## 🚀 Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Vue.js 3, TypeScript, Vite |
| Backend | .NET 10, ASP.NET Core Web API |
| Hosting | Azure Static Web Apps (frontend), Azure App Service (backend) |
| CI/CD | GitHub Actions |
| Planned: Auth | Azure AD B2C |
| Planned: Database | Azure SQL |
| Planned: Banking | Open Banking API (e.g. TrueLayer) |

---

## 🏗️ Architecture

### Current

```mermaid
graph TD
    User["👤 User (Browser)"]
    
    subgraph Azure [&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Azure</b>]
        Frontend["Azure Static Web Apps\n(Vue.js Frontend)"]
        Backend["Azure App Service\n(.NET 10 API)"]
    end

    subgraph GitHub [<b>Github</b>]
        Repo["GitHub Repository"]
        Actions["GitHub Actions\nCI/CD Pipeline"]
    end

    User -->|HTTPS| Frontend
    Frontend -->|REST API calls| Backend
    Repo -->|Push to main| Actions
    Actions -->|Deploy frontend| Frontend
    Actions -->|Deploy backend| Backend
```

### Planned

```mermaid
graph TD
    User["👤 User (Browser)"]
    
    subgraph External [<b>External</b>]
        BankAPI["🏦 Open Banking API\ne.g. TrueLayer"]
    end

    subgraph Azure [<b>Azure</b>]
        Frontend["Azure Static Web Apps\n(Vue.js Frontend)"]
        Backend["Azure App Service\n(.NET 10 API)"]
        Auth["Azure AD B2C\n(Authentication)"]
        Database["Azure SQL Database"]
    end

    subgraph GitHub [<b>Github</b>]
        Repo["GitHub Repository"]
        Actions["GitHub Actions\nCI/CD Pipeline"]
    end

    User -->|HTTPS| Frontend
    User -->|Login| Auth
    Auth -->|Token| Frontend
    Frontend -->|Authenticated API calls| Backend
    Backend -->|Fetch transactions| BankAPI
    Backend -->|Read/Write| Database
    Repo -->|Push to main| Actions
    Actions -->|Deploy frontend| Frontend
    Actions -->|Deploy backend| Backend
```

### Planned Authentication & Data Flow

```mermaid
sequenceDiagram
    actor User
    participant Frontend as Vue Frontend
    participant Auth as Azure AD B2C
    participant Backend as .NET API
    participant Bank as Open Banking API
    participant DB as Azure SQL Database

    User->>Frontend: Visit app
    Frontend->>Auth: Redirect to login
    User->>Auth: Enter credentials
    Auth->>Frontend: Return JWT token
    
    User->>Frontend: View transactions
    Frontend->>Backend: GET /api/transactions (with token)
    Backend->>Auth: Validate token
    Auth->>Backend: Token valid
    Backend->>Bank: Fetch transactions
    Bank->>Backend: Return transactions
    Backend->>DB: Cache transactions
    Backend->>Frontend: Return transactions
    Frontend->>User: Display transactions
```

---

## ✨ Features

### Current
- Transaction list fetched from API and displayed in the browser
- Mock transaction data with date, description, amount and category
- Error and loading states handled in the frontend

### Planned
- User authentication via Azure AD B2C
- Real bank account integration via Open Banking API
- Monthly spending summaries
- Spending breakdown by category
- Budget setting and tracking
- Azure SQL database for persistence

---

## 🛠️ Running Locally

### Prerequisites
- .NET 10 SDK
- Node.js 18+
- npm

### Backend
```bash
cd backend/BudgetTracker.Api
dotnet run
```

### Frontend
```bash
cd frontend/budget-tracker
npm install
npm run dev
```

The frontend runs on `http://localhost:5173` and the backend on `https://localhost:7259`.

---

## 🚢 Deployment

Both the frontend and backend are automatically deployed to Azure on every push to `main` via GitHub Actions.

- Frontend: Azure Static Web Apps
- Backend: Azure App Service