using BlossomTest.Application.Entities.Applications.Commands.Create;

namespace BlossomTest.Application.Entities.Application.Commands.Create;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{    
    public CreateApplicationCommandValidator()
    {        
        RuleFor(x => x.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(x => x.ClientAccountId)
            .NotEmpty();
    }
}