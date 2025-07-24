.NET 8. 
Based Clean Architecture. 
Template leveraging Minimal API.

Structured Application Architecture
The solution follows a layered approach, separating concerns across domain, application, infrastructure, and API layers. It implements CQRS for improved clarity and scalability of business logic.

Domain-Driven Design (DDD)
Core business logic is modeled using rich domain models: Entities, Value Objects, Domain Events, Specifications, and Domain Services — enabling better encapsulation and behavior-driven design.

Minimal API Approach
Utilizes .NET Minimal APIs to provide lightweight, performant HTTP endpoints with minimal overhead, simplifying the development of RESTful services.

Flexible Database Integration
Support for SQL Server, PostgreSQL, or other relational databases can be added with minimal configuration changes.

Built-In Resilience and Fault Tolerance
Incorporates Polly for handling transient faults via retry policies, circuit breakers, and fallback mechanisms — increasing reliability of external system communication.


bash
//dotnet ef migrations add InitialCreate --project BlossomTest.Infrastructure.Persistence --startup-project BlossomTest.Presentation -- output-dir Migrations
//dotnet ef database update --project BlossomTest.Infrastructure.Persistence --startup-project BlossomTest.Presentation
