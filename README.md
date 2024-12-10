# Task-Management-System

A RESTful API built with .NET Core for managing tasks. This application allows users to create, read, update, delete tasks, apply filters, and export task data as a CSV file. It also includes Swagger documentation for easy exploration of the API.

**Features**
  CRUD Operations: Manage tasks with endpoints for creating, reading, updating, and deleting tasks.
  Filters: Retrieve tasks filtered by status, due date, and pagination.
  CSV Export: Export all tasks into a CSV file.
  Validation: Input validation and meaningful error messages.
  Swagger Integration: Explore and test API endpoints interactively.
  Unit Tests: xUnit test coverage for core functionality.

**Prerequisites**
.NET 6 SDK or later
Any IDE or text editor (e.g., Visual Studio Code)
Git

**Setup Instructions**
git clone https://github.com/priyansh0079/Task-Management-System.git
cd Task-Management-System

cd TaskManagementSystem.API
dotnet restore
dotnet build
dotnet run

cd TaskManagementSystem.Tests
dotnet test

