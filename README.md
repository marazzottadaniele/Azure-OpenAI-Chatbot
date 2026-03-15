# Azure OpenAI ChatBot

A chatbot application with ASP.NET Core API backend that integrates with Azure OpenAI. The backend is complete with configurable settings, while the Angular frontend is planned for future development.

## Features

- Clean Architecture with Domain, Application, Infrastructure, and API layers
- Azure OpenAI integration for conversations
- Configurable chatbot settings (personality, context, limits)
- Settings persistence to JSON files
- REST API with Swagger documentation
- Input validation and error handling

## Architecture

`
ChatBotV2/
   ChatBotV2.Domain/                      # Business entities and rules
   ChatBotV2.Application/                 # Use cases and business logic
   ChatBotV2.Infrastructure/              # External services (OpenAI, storage)
   ChatBotV2.Api/                         # ASP.NET Core Web API
   ChatBotV2.Frontent/chatbot-frontend    # Frontend UI
`

## Tech Stack

### Backend
- ASP.NET Core 8.0
- Azure OpenAI
- Swashbuckle (API documentation)
- DotNetEnv (environment variables)

### Frontend
- Angular
- TypeScript

## Prerequisites

- .NET 8.0 SDK
- Azure OpenAI resource

## Getting Started

### Backend Setup

1. Clone the repository
2. Create .env file in ChatBotV2.Api/:
   `
   AzureOpenAI__Endpoint=https://your-resource.openai.azure.com/
   AzureOpenAI__Key=your-api-key-here
   AzureOpenAI__Deployment=gpt-4o-mini
   `
3. Run: cd ChatBotV2.Api && dotnet run
4. Access: https://localhost:7xxx/swagger/index.html

### Frontend (Coming Soon)

The Angular frontend will include:
- Admin dashboard for settings configuration
- Chat interface
- Settings management

## API Endpoints

### Entities

ChatbotSettings class (
   string   VoiceTone
   string   Context
   int      MaxWords
   string   SystemMessage
)

HistoryRole enum (
   System
   User
   Assistant
)

HistoryMessage record (
   HistoryRole    Role
   string         Message
   DateTimeOffset Timestamp
)

ChatRequest record(
   string   Message
)

ChatRequestDto(
   ChatRequest       Request 
   HistoryMessage[]  History 
)

ChatRequestDto(
   string   Content
   DateTime Timestamp
   int      WordCount
)


### Routes

- GET    /api/settings  - Get settings
   input:   none
   output:  ChatbotSettings
- PUT    /api/settings  - Update settings
   input:   ChatbotSettings
   output:  ChatbotSettings
- POST   /api/chat      - Send chat message
   input:   ChatRequestDto
   output:  ChatResponseDto

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit changes
4. Push to branch
5. Open a Pull Request

## License

MIT License
