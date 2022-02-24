## Installation
Clone from GitHub
```git bash
git clone https://github.com/oztrkkaan/Contact-CleanArchitectureSample
```
## Database Migration
*Make sure connection string is correct*
<br><br>
To build database run in PMC:
```git bash
dotnet ef migrations add InitialMigration --project src/Contact.Persistence
```
and
```git bash
dotnet ef database update --project src/Contact.Persistence
```
When you start API project Swagger interface will redirect you about endpoint. <br>
If you want to create report files with RabbitMQ, don't forget start RabbitMQReportConsumer console app.

## Technologies
- .Net 6.0
- AspNetCore WebAPI
- Console App
- xUnit
- EntityFrameworkCore
- FluentValidation
- MediatR
- AutoMapper
- PostgreSQL
- ClosedXML
- RabbitMQ

