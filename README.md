HackerApiConnector.API

This project is a .NET 6 Web API built with Domain-Driven Design (DDD). It consumes Hacker News data and uses Redis (Memurai on Windows) for distributed caching.

Technologies
- .NET 6
- ASP.NET Core Web API
- AutoMapper
- StackExchange.Redis
- Memurai (Redis for Windows)

Installation
- Install .NET 6 SDK.
- Install Memurai on Windows (Server to Redis):
- Download from https://www.memurai.com/download
- Install as a Windows service.
- Restore packages
- Run the API

Testing the Cache
- Call endpoint: GET /api/stories/{n}
- First call: fetches Hacker News and stores in Redis.
- Subsequent calls: returns cached data quickly.
- Check Redis keys: redis-cli keys * Example: HackerApiCache_beststories_2
- Wait for expiration (default 1 minute). Key disappears, next call fetches again.

Example Usage
GET https://localhost:5001/api/stories/5
Response: [ { "id": 12345, "title": "Example story", "score": 150, "url": "https://news.ycombinator.com/item?id=12345" } ]

======================================================================================

Memurai Troubleshooting Guide
Sometimes Memurai may block write commands with errors like:
MISCONF Memurai is configured to save RDB snapshots, but it's currently unable to persist to disk.
Commands that may modify the data set are disabled, because this instance is configured to report errors
during writes if RDB snapshotting fails (stop-writes-on-bgsave-error option).

Step-by-step fixes:
- If you see MISCONF during execution: edit memurai.conf (C:\Program Files\Memurai\memurai.conf) and set stop-writes-on-bgsave-error no, then restart Memurai service (net stop memurai && net start memurai).
- If CONFIG SET is not available ("admin mode required"): change the setting directly in memurai.conf instead of using CLI or code, then restart Memurai service.


