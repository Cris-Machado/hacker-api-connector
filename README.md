Hacker API Connector
Hacker API Connector is a lightweight .NET 6 solution designed to fetch and cache Hacker News “best” stories. It provides application services to efficiently retrieve detailed story information.
The solution follows a layered architecture, organized into:
- HackerApiConnector.API
- HackerApiConnector.Application
- HackerApiConnector.Domain
- HackerApiConnector.Infrastructure

✨ Features
- Fetches Hacker News best stories via a REST-based HTTP client.
- Implements in-memory caching at both the index and per-story level using IMemoryCache.
- Maps domain models to view models with AutoMapper.
- Ensures clean separation of concerns through interfaces for HTTP clients and request services.

🛠 Technologies
- .NET 6
- C# 10
- AutoMapper
- Microsoft.Extensions.Caching.Memory (IMemoryCache)
- HttpClient (wrapped via IHackerApiHttpClient)
- Dependency Injection using the built-in ASP.NET Core DI container

📂 Project Structure
- HackerApiConnector.API – Web API project (hosting and configuration).
- HackerApiConnector.Application – Application services, e.g., ConnectorService implementing IConnectorService.
- HackerApiConnector.Domain – Domain models, view models, and contracts (interfaces).
- HackerApiConnector.Infrastructure – REST client implementations and HTTP client wrappers.

🚀 Getting Started
Prerequisites
- .NET 6 SDK
- Visual Studio 2022 or dotnet CLI
Clone the repository and follow the setup instructions to get started.
