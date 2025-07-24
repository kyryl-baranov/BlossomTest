namespace BlossomTest.Application.Entities.Applications.Commands.Update;

public record UpdateApplicationCommand(int Id, string Name, int ClientAccountId) : IRequest<Result>;