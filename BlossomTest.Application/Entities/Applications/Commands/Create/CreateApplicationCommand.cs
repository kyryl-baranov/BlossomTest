using System.ComponentModel.DataAnnotations;

namespace BlossomTest.Application.Entities.Applications.Commands.Create;

public record CreateApplicationCommand(
    [property: Required, MaxLength(100)] string Name,
    [property: Required] int ClientAccountId) : IRequest<Result<int>>;