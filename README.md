# ESubmissionSystem 🎓

An enterprise-grade academic submission tracking system built with ASP.NET Core MVC and Entity Framework Core. This project demonstrates strong typing, object-relational mapping (ORM), and strict architectural separation of concerns.

## 🌟 Key Features

* **MVC Architecture:** Clean separation of routing (`Controllers`), business data (`Models`), and interface rendering (`Views`)[cite: 2].
* **Robust Authentication:** Integrated identity management including user registration, login, two-factor authentication (2FA), and password recovery mechanisms[cite: 2].
* **Object-Relational Mapping:** Database interactions abstracted via Entity Framework Core (`ApplicationDbContext`)[cite: 2].
* **Code-First Migrations:** Automated database schema generation and version control tracking[cite: 2].

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 2.0[cite: 2]
* **Language:** C#
* **ORM:** Entity Framework Core
* **Frontend:** Razor Pages (.cshtml), HTML5, CSS3, Bootstrap[cite: 2]

## 📂 Project Architecture

The repository strictly follows the standard ASP.NET Core MVC pattern:
* `Controllers/` - Functional request handlers and business logic coordinators (e.g., `SubmissionController`)[cite: 2].
* `Models/` - Domain entities and strongly-typed ViewModels ensuring secure data transfer[cite: 2].
* `Views/` - Decoupled Razor markup templates for dynamic UI generation[cite: 2].
* `Data/Migrations/` - Code-first execution tracking snapshots for database schema management[cite: 2].

## ⚙️ Local Installation

1. Clone the repository:
   `git clone https://github.com/AliTudaSuda/ESubmissionSystem.git`
2. Ensure you have the .NET Core SDK installed.
3. Navigate to the project directory in your terminal.
4. Restore NuGet packages:
   `dotnet restore`
5. Apply database migrations to create the local SQLite/SQL Server schema:
   `dotnet ef database update`
6. Run the application:
   `dotnet run`

## 🚀 Upcoming Modernization Plan
*This application is currently scheduled for a major infrastructure upgrade:*
* **Framework Migration:** Upgrading the target runtime from `.NET Core 2.0` to a modern Long-Term Support (LTS) version like `.NET 8.0` to leverage performance enhancements and current security protocols[cite: 2].
* **Overposting Protection:** Further decoupling domain entities from input actions using dedicated Data Transfer Objects (DTOs) and anti-forgery tokens.