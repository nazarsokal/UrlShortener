# URL Shortener

## Overview

UrlShortener is a full-stack web application for creating and managing shortened URLs. It features secure JWT-based authentication with user roles and a responsive React frontend. The backend is built with ASP.NET Web API and MS SQL Server, making it easy to deploy and scale.

---

## Prerequisites

- **Node.js and npm** (for client)
- **.NET 8 SDK or newer** (for API)
- **Docker** (for running SQL Server database)
- **EF Core CLI** (optional, for DB migrations)

---
## Backend Setup (ASP.NET Core API)

### 1. Clone the Repository

```bash
git clone https://github.com/nazarsokal/UrlShortener.git
cd UrlShortener
```
### 2. Restore Dependencies

```bash
dotnet restore
```

---

## SQL Server Setup with Docker

To run db in Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Very123StrongPass" -p 1433:1433 --name url-shortener-sqlserver  -d mcr.microsoft.com/mssql/server:2022-latest
```

- Username: `sa`  
- Password: `Very123StrongPass`  
- Port: `1433`

---

## Configure Connection String

In `appsettings.json` of the API project, update your connection string like this:

```json
"ConnectionStrings": {
  "DefaultConnection" : "Server=localhost,1433;Database=master;User Id=SA;Password=Very123StrongPass;TrustServerCertificate=True;"
}
```
---

## Apply EF Core Migrations

To generate a new migration:

```bash
dotnet ef migrations add InitialCreate --output-dir Infrastructure/Migrations
```

To create the database:
```bash
dotnet ef database update
```

---

## Run the API

Start the API:

```bash
dotnet run
```
---
## Frontend Setup (React)

### 1. Unzip and Open the API Project

Unzip and navigate to the API folder:
```bash
cd /UrlShortener.UI/url-shortener-client
```
### 2. Install Dependencies

```bash
npm install --legacy-peer-deps
```

> The `--legacy-peer-deps` flag resolves potential dependency conflicts.

### 3. Run React App

```bash
npm start
```

Visit: [http://localhost:5173](http://localhost:5173)

---
