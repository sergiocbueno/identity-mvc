# Identity MVC

This is a base project application for User Authentication and Autherization in Asp.Net Core MVC with Identity UI.

### Technical requirements

- ASP.NET MVC Core 6
- Docker
    - mcr.microsoft.com/dotnet/sdk:6.0 (image)
    - mcr.microsoft.com/dotnet/aspnet:6.0 (image)
    - postgres:14.1-alpine (image)
- Google Cloud Platform
    - Cloud Build
    - Cloud Storage
    - Cloud Run
    - Container Registry (Images)
- PostgreSQL
- Entity Framework (including Migrations for versioning)

### Migrate Database

This project has been using Entity Framework as an object-database mapper for PostgreSQL. To create a change in database (data or structure) simply follow the commands below:

1. Inside AuthSystem directory
2. dotnet ef migrations add *[MigrationName]* -o Migrations

After version 1.30 the application initialization was optimized and the entity framework migration stopped to be initialized together with the application. A new API was created, PUT /api/migrations, which triggers now the EF migration. This new API request was included in the continuous delivery last step.

### Pipelines

This project contains continuous integration (CI - Github) and continuous deployment (CD - Google Cloud Platform).
Basically for all branches, after a pull request is created to master or a push directly into master, a pipeline is trigged which checks all packages, build the solution, and run all unit tests.

*On the master branch, there is one more step called 'release' where the pipeline automatically creates a tag and a release inside Github, which also automatically triggers an application deployment inside Google Cloud Platform environment using Docker integration*

### Manually commands

- Resolve solution dependencies:
    1. dotnet restore
- Build solution:
    1. dotnet build
- Publish portfolio application:
    1. Inside AuthSystem directory
    2. dotnet publish -c *[publish mode e.g. Release]* -o *[destination folder]*
- Create local PostgreSQL (inside Container)
    1. Inside AuthSystem directory
    2. docker-compose up -d