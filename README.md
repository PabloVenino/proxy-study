
# WebScrapApp Documentation

## Overview

**WebScrapApp** is a web scraping application built using C# with .NET 8. The application utilizes MITMProxy, and the main goal is to capture and extract information from websites, storing the data in a SQL Server database.
Disclaimer: You can store whatever SQL database you want, in this one I'm using Stored Procedures. You may need chaging the packages using the Command Line or NuGet.

## Project Structure
```
WebScrapApp/
│
├── Application/
│   ├── Abstractions/
│   │   └── ISqlConnectionFactory.cs
│   └── ApplicationModule.cs
│
├── Infra/
│   ├── Persistence/
│   │   └── SqlConnectionFactory.cs
│   └── InfraModule.cs
│
├── Core/
│   ├── Models/
│   │   └── ApiResponseDto.cs
│   
└── WebScrapApp/
    └── Program.cs (main entry point)
```

## Components Description

- **Application/Abstractions/ISqlConnectionFactory.cs**: Defines the interface for SQL connection factory to manage database connections.
- **ApplicationModule.cs**: Creates a Method for easily Depency Injection.
  
- **Infra/Persistence/SqlConnectionFactory.cs**: Implements the SQL connection factory, managing connection creation to the SQL Server database.
- **InfraModule.cs**: Creates a Method for easily Depency Injection.

- **App/Program.cs**: Entry point of the application, configuring and running the application.

## Technologies Used

- **.NET 8**: The latest version of .NET, providing enhanced performance and modern language features.
- **SQL Server**: The database used for storing scraped data.
- **MITMProxy**: A proxy tool used to intercept the connection between our localserver and the target.

## How It Works

1. **MITMProxy Integration**: The application uses MITMProxy to manipulate web traffic from a specified website.
2. **Data Extraction**: The captured data is processed and extracted by the application.
3. **Data Storage**: Extracted data is then saved into a SQL Server database using a connection managed by `SqlConnectionFactory`.

## Getting Started

1. **Setup MITMProxy**: You'll need a related Proxy for doing a lot of requests without blocking your IP, so I suggest you to use a MITMProxy lookalike.
2. **Run the Application**: Execute the application using the following command: (Or just run it in Visual Studio :) )
   ```
   dotnet run --project WebScrapApp
   ```
3. **Database Connection**: The application will connect to the SQL Server as configured in `SqlConnectionFactory`, using your ConnectionString, from the Program.cs

## Future Enhancements

None in mind, yet
