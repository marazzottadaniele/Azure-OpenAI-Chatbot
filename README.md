# ChatBot V2

This is an expanded version of the configurable chatbot application, rebuilt with ASP.NET Core Web API backend and Angular frontend.

## Project Structure

```
ChatBotV2/
├── Backend/          # ASP.NET Core Web API
│   ├── Program.cs    # API endpoints for chat and settings
│   ├── appsettings.json
│   └── Backend.csproj
└── Frontend/         # Angular SPA
    ├── src/
    │   ├── app/
    │   │   ├── admin/        # Settings dashboard
    │   │   ├── chat/         # Chat interface
    │   │   └── api.service.ts # API client
    │   └── main.ts
    └── package.json
```

## Features

- **Admin Dashboard**: Configure chatbot settings (voice tone, context, max words limit)
- **Chat Interface**: Interact with Azure OpenAI using configured settings
- **Two-phase workflow**:
  1. Customization phase: Set up chatbot behavior via admin dashboard
  2. Chat phase: Use the configured chatbot for conversations

## Prerequisites

- .NET 8 SDK
- Node.js 18+
- Azure account with OpenAI deployment

## Backend Setup

1. Configure `Backend/appsettings.json`:
```json
{
  "AzureOpenAI": {
    "Endpoint": "https://your-resource.openai.azure.com/",
    "Deployment": "gpt-4o-mini",
    "Key": "your-api-key"
  }
}
```

2. Install dependencies:
```bash
cd Backend
dotnet restore
```

3. Run the server:
```bash
dotnet run
```

The API will be available at `http://localhost:5000` (or port specified in launchSettings.json)

## Frontend Setup

1. Install dependencies:
```bash
cd Frontend
npm install
```

2. Start development server:
```bash
ng serve
```

Open browser at `http://localhost:4200`

## API Endpoints

- `GET /api/settings` - Retrieve current chatbot settings
- `PUT /api/settings` - Update chatbot configuration
- `POST /api/chat` - Send message to chatbot

## Usage

1. Navigate to Admin Dashboard (`/admin`)
2. Configure voice tone, context, and max word limit
3. Save settings
4. Go to Chat (`/chat`)
5. Converse with the configured chatbot