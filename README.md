# EmpMS
Employee Management System Hands-on

## 📌 Project Overview

EmpMS is a full-stack Employee Management System built using Angular for the frontend, .NET 8 Web API for the backend, and SQL Server for data storage. The application is designed to manage employee data efficiently and securely, with separate interfaces for administrators and employees.

---

## 🧩 Features

### 👨‍💼 Admin Panel
- ➕ Add new employees
- ✏️ Edit employee details
- ❌ Delete employees
- 📋 View list of all employees

### 👨‍💻 Employee Panel
- 📝 Register and login
- 👤 View personal profile
- 📱 Update phone number and address

---

## 🛠️ Tech Stack

| Layer       | Technology         |
|-------------|--------------------|
| Frontend    | Angular            |
| Backend     | .NET 8 Web API     |
| Database    | SQL Server         |

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio
- Node.js
- .NET 8 SDK
- SQL Server
- Git
- SSMS

### Setup Instructions

#### 1. Clone the repository
```bash
git clone https://github.com/Nitin-Rana-CTS/EmpMS.git
```
#### 2. Setup dependencies for backend
<!-- - Microsoft.EntityFrameworkCore -->
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore

- dotnet list package --include-transitive
- dotnet restore : The `dotnet restore` command uses NuGet to restore dependencies and project-specific tools specified in the project file. It downloads and installs any missing packages required to build the project.


#### 2. Setup for local sql server
- sqllocaldb create MSSQLLocalDB
- sqllocaldb start MSSQLLocalDB

- sqllocaldb stop MSSQLLocalDB
- sqllocaldb delete MSSQLLocalDB


#### 3. Connection string and setting dbcontexts in program.cs
- appsettings.json :
    {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "AppDbConnection": "Server=(localdb)\\EMSLocalDBInstance;Database=EMPMS_AppDB;Trusted_Connection=True;",
    "AuthDbConnection": "Server=(localdb)\\EMSLocalDBInstance;Database=EMPMS_AuthDB;Trusted_Connection=True;"
  }
}

-builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection"))
);
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnection"))
    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)) // to ignore model change warnings
);


#### 4. Connect to SSMS
- enter server name : (localdb)\EMSLocalDBInstance
- use windows authentication

#### 5. 🔐 Identity Configuration

To enable user and role management along with secure token generation (e.g., for password reset, email confirmation), the following Identity setup is used (not the AddIdentityCore):

```csharp
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
