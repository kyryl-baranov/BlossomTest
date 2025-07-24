namespace BlossomTest.Application.Entities.Applications.Queries.Get;

public record GetApplicationQuery(int Id) : IRequest<Result<ApplicationResponse>>;