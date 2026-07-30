# DemoPlatform

A multi-service ASP.NET Core (.NET 10) solution that mimics a production microservices architecture with JWT authentication, an API gateway, and a shared class library.

## Services

| Service | Port | Description |
|---------|------|-------------|
| **AuthSite** | 5001 | Authentication service. Validates credentials and issues signed JWT tokens. |
| **Hub** | 5002 | API gateway / entry point. Proxies protected requests to AppOne, forwarding the `Authorization` header. |
| **AppOne** | 5003 | Protected business API. Requires a valid JWT issued by AuthSite to access `/api/data`. |
| **Lib-AppBase** | — | Shared class library with `ApiResponse<T>`, JWT settings, token generation, and JWT Bearer validation extensions. |

### Test credentials

- **Username:** `admin`
- **Password:** `password123`

## Project structure

```
DemoPlatform/
├── DemoPlatform.sln
├── docker-compose.yml
├── README.md
└── src/
    ├── Lib-AppBase/          # Shared JWT + ApiResponse library
    ├── AuthSite/             # Login + JWT issuance
    ├── Hub/                  # Gateway / proxy
    └── AppOne/               # Protected API
```

## Run locally with dotnet

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

Open **three separate terminals** from the repository root:

```bash
# Terminal 1 — AuthSite
dotnet run --project src/AuthSite/AuthSite.csproj

# Terminal 2 — AppOne
dotnet run --project src/AppOne/AppOne.csproj

# Terminal 3 — Hub
dotnet run --project src/Hub/Hub.csproj
```

Swagger UI is available in Development:

- AuthSite: http://localhost:5001/swagger
- Hub: http://localhost:5002/swagger
- AppOne: http://localhost:5003/swagger

## Run with Docker Compose

Build and start all three web services:

```bash
docker-compose up --build
```

Services are exposed on the same host ports: **5001** (AuthSite), **5002** (Hub), **5003** (AppOne).

## Test the full flow

### 1. Health checks

```bash
curl http://localhost:5001/health
curl http://localhost:5002/health
curl http://localhost:5003/health
```

### 2. Login via AuthSite (get JWT)

```bash
curl -s -X POST http://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password123"}'
```

Example response:

```json
{
  "success": true,
  "message": "Login successful.",
  "data": {
    "token": "<JWT_TOKEN>",
    "tokenType": "Bearer",
    "expiresInMinutes": 60
  }
}
```

Copy the `token` value from the response.

### 3. Call protected data through Hub (with token)

```bash
export TOKEN="<JWT_TOKEN>"

curl -s http://localhost:5002/api/data \
  -H "Authorization: Bearer $TOKEN"
```

Expected response:

```json
{
  "message": "Protected data",
  "timestamp": "2026-07-30T12:00:00.0000000Z",
  "user": "admin"
}
```

### 4. Verify protection (no token → 401)

```bash
curl -i http://localhost:5002/api/data
```

### 5. Hub welcome endpoint

```bash
curl http://localhost:5002/
```

## JWT configuration

`appsettings.json` uses a placeholder — do not commit real secrets there:

```json
"Jwt": {
  "SecretKey": "REPLACE_VIA_ENV_VARIABLE",
  ...
}
```

For **local `dotnet run`** (Development), the secret is set in `appsettings.Development.json` on AuthSite and AppOne.

For **Docker Compose**, set `JWT_SECRET_KEY` or rely on the default dev value:

```bash
export JWT_SECRET_KEY="your-shared-secret"
docker compose up --build
```

AuthSite and AppOne must use the **same** secret so tokens validate correctly.

## Build the solution

```bash
dotnet build DemoPlatform.sln
```
