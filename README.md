# 📅 AI Calendar WebAPI

**AI Calendar** is an ASP.NET Core WebAPI for managing personal calendar events. It supports natural language prompts, multi-user scheduling, and can integrate with LLMs via MCP. Backend is deployed on Render, frontend on GitHub Pages.

## 🚀 Features

- 👤 Simple user authentication
- 📆 Event CRUD: create, update, delete events
- 👥 Manage participants for events
- 🔍 Find the earliest available slot for all participants
- 💬 Natural Language Prompt Support:
  - `"Add meeting with Anna on Friday from 10:00 to 11:00"`
  - `"Cancel all events with title 'Coffee break'"`

## 🛠️ Tech Stack

- **Backend:** .NET 8 ASP.NET Core WebAPI
- **LLM Integration:** PromptProcessor + MCP server
- **Frontend:** React + Vite + TypeScript, FullCalendar
- **Swagger / OpenAPI** for testing
- **Docker** for deployment

## 🔗 Links

- 🖥️ Backend API: [Render](https://aicalendar-gqcp.onrender.com)

- 📘 Swagger UI: [Swagger](https://aicalendar-gqcp.onrender.com/swagger)

- 💻 Frontend GitHub: [ai-calendar-ui](https://github.com/OlesiaKubska/ai-calendar-ui)

- 🌐 Frontend Live [GitHub Pages deployment](https://olesiakubska.github.io/ai-calendar-ui/)

## 📌 Main Endpoints

### 📑 Events

```http
GET    /api/v1/events
GET    /api/v1/events/{id}
POST   /api/v1/events
PUT    /api/v1/events/{id}
DELETE /api/v1/events/{id}
```

### 👥 Event Participants

```http
GET    /api/v1/events/{eventId}/participants
POST   /api/v1/events/{eventId}/participants
PUT    /api/v1/events/{eventId}/participants/{userId}
DELETE /api/v1/events/{eventId}/participants/{userId}
```

### 💬 Prompt Executor

```http
POST /api/v1/events/prompt
```

### 🧪 Free Slot Finder

```http
POST /api/v1/events/find-slot
```

### 🧠 Architecture (MCP + LLM Integration)

- Users interact via Console / Web / Native apps
- Prompts go to MCP Server
- MCP Server calls your WebAPI
- WebAPI performs scheduling logic and CRUD actions
- Optionally integrates with Google, Microsoft, Apple Calendars

### 🌐 Frontend UI

- React + TailwindCSS + Vite
- FullCalendar for event display
- PromptExecutor.tsx for entering natural language instructions

### 🧪 Testing

✅ Unit-tested free-slot finder algorithm (TimeSlotFinderService)

✅ Prompt processor tested manually and via Swagger

### ⚙️ Getting Started

#### Backend

```
# 1. Clone the repo
git clone https://github.com/OlesiaKubska/ai-calendar.git
cd ai-calendar

# 2. Start the backend
cd AICalendar
dotnet run
```

#### You can also run via Docker:

```
docker build -t aicalendar .
docker run -p 8080:8080 aicalendar
```

#### Frontend (ai-calendar-ui)

```
git clone https://github.com/OlesiaKubska/ai-calendar-ui.git
cd ai-calendar-ui
npm install
npm run dev
```

### ✅ Completed Tasks

- Full WebAPI for events and participants
- Prompt endpoint for LLM integration
- Frontend UI for calendar and prompt
- Free-slot search algorithm
- Unit testing
- Docker support
- Deployed backend (Render) & frontend (GitHub Pages)

# 👩‍💻 Author

Olesia Kubska

- 🔗 GitHub: github.com/OlesiaKubska
- 📧 Email: kublesia0908@gmail.com
