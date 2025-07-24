namespace BlossomTest.Application.Entities.Applications.Commands.Update;

public class UpdateApplicationCommandValidator : AbstractValidator<UpdateApplicationCommand>
{
    
    public UpdateApplicationCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.ClientAccountId)
            .NotEmpty();
    }
}