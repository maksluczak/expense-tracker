# Expense Tracker

A personal finance tracking app built with **.NET** (backend) and **Angular** (frontend).

---

## Tech Stack

- .NET 8 SDK
- Docker
- Angular
- PostgreSQL 

---

## 1. Start the Database (Docker)

```bash
docker-compose up -d
```

---

## 2. Start the Backend (.NET)

Navigate to the backend project folder and run:

```bash
cd src/ExpenseTracker.Api
dotnet run
```

The API will start on:

```
https://localhost:7123
http://localhost:5123
```

---

## 3. Start the Frontend (Angular)

In a **separate terminal**, navigate to the frontend folder:

```bash
cd expense-tracker-client
npm install
ng serve
```

The app will be available at [http://localhost:4200](http://localhost:4200).

The Angular dev server proxies all `/api` requests to the .NET backend automatically via `src/proxy.conf.json`.

---