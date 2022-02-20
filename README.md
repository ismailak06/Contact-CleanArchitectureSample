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

## Technologies
- .Net 6.0
- AspNetCore WebAPI
- Console App
- EntityFrameworkCore
- FluentValidation
- MediatR
- AutoMapper
- PostgreSQL
- ClosedXML

