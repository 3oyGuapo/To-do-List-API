# To-Do List Application

This To-do List Application is a desktop and backend app use for managing personal and team tasks. This app allows to add, read, update(change tasks and update status) and delete tasks. This project demonstrates a decoupled architecture with a WPF desktop client and an ASP.NET Core Web API backend.


## Features

*   **Full-Stack Architecture**: Separation of concerns between Client (WPF), Server (API), and Data (PostgreSQL).
*   **RESTful API**: Designed using ASP.NET Core with standard HTTP methods and status codes.
*   **Data Persistence**: Utilizes Entity Framework Core (Code-First) for database management.
*   **Responsive Client**: WPF application with MVVM-style pattern and asynchronous data binding.
*   **Reliability**: Includes Unit Tests using xUnit and EF Core InMemory provider.

## Tech Stack

*   **Backend**: C#, .NET 8, ASP.NET Core Web API
*   **Frontend**: WPF 
*   **Database**: PostgreSQL
*   **ORM**: Entity Framework Core
*   **Testing**: xUnit
*   **Tools**: Visual Studio 2022, Swagger/OpenAPI

## Getting Started

Follow these steps to get the project running on your local machine.

### Prerequisites

*   .NET 8.0
*   PostgreSQL
*   Visual Studio 2022

### Installation

1.  **Clone the repository**
    ```bash
    git clone https://github.com/3oyGuapo/To-do-List-API.git
    ```

2.  **Database Setup**
    *   Ensure PostgreSQL is running.
    *   Update the connection string in `TodoApi/appsettings.json` with your database credentials.
    *   Navigate to the API project and run migrations to create the database:
        ```bash
        cd "To-do List API"
        dotnet ef database update
        ```

3.  **Run the Backend (API)**
    *   Open the solution in Visual Studio.
    *   Set `TodoApi` as the startup project (or use Multiple Startup Projects).
    *   Run the project. 

4.  **Run the Client (WPF)**
    *   Ensure the API is running.
    *   Start the `To_do_List_Desktop` project.

## Running Tests

To run the unit tests for the backend logic:

1.  Open the **Test Explorer** in Visual Studio.
2.  Run All Tests.
