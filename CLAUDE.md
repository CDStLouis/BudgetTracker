# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BudgetTracker is a full-stack personal finance app: a Vue 3 + TypeScript frontend and an ASP.NET Core (.NET 10) backend, deployed to Azure Static Web Apps + Azure App Service.

## Commands

### Frontend (`frontend/budget-tracker/`)
```bash
npm run dev          # Dev server at http://localhost:5173
npm run build        # Type-check + Vite build
npm run test         # Vitest watch mode
npm run test:run     # Vitest single run (used in CI)
```

### Backend (`backend/BudgetTracker.Api/`)
```bash
dotnet run           # Dev server at https://localhost:7259
dotnet build         # Build only
```

### Running a single test
```bash
npx vitest run src/components/MonthSelector.spec.ts
```

## Architecture

### Frontend-Backend Communication
- In **development**: Vite proxies `/api/*` to `http://localhost:5103` (configured in `vite.config.ts`). The backend must be running separately.
- In **production**: `VITE_API_URL` env var (from `.env.production` or CI secret) points to the Azure App Service URL.
- The frontend calls `fetch('/api/transactions')` — no API client library.

### Frontend State & Routing
There is **no Vue Router** — screen transitions are handled by `activeScreen` and `activeView` refs in `TransactionsView.vue`, which acts as the single smart orchestrator component. All other components are presentational.

State is managed with Vue Composition API (`ref`, `computed`, `watch`) — no Pinia or Vuex.

Component hierarchy:
```
TransactionsView (smart, owns all state)
├── TransactionDetailView   (detail screen)
├── LineGraphView           (graph view mode)
└── table view mode
    ├── MonthSelector       (receives disablePrevious/disableNext props)
    ├── ViewToggle
    └── DateGroup → TransactionRow
```

Available months are **derived from transaction data**, not hardcoded. Month navigation bounds (`disablePrevious`, `disableNext`) are computed in `TransactionsView` and passed as props down to `MonthSelector`.

### Backend
`MockTransactionService` is the only data source — there is no real database yet. The `Transaction` model uses negative `Amount` for expenses and positive for income. The frontend converts this to a `type: 'expense' | 'income'` field when mapping.

### Data Flow
`GET /api/transactions` → `TransactionsView` fetches on mount → groups by month and date → passes filtered slice to child components.

## Testing
Tests use **Vitest** + `@vue/test-utils` with jsdom. Test files live next to their components (`.spec.ts`). CI runs `npm run test:run` before building the frontend.

## CI/CD
Two GitHub Actions workflows:
- **Frontend**: runs tests → Vite build → deploys to Azure Static Web Apps
- **Backend**: `dotnet build` (Release) → deploys to Azure App Service

Both trigger on push to `main`.
