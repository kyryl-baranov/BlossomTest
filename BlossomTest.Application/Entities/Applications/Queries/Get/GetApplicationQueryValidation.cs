namespace BlossomTest.Application.Entities.Applications.Queries.Get;

public class GetApplicationQueryValidation : AbstractValidator<GetApplicationQuery>
{    
    public GetApplicationQueryValidation()
    {
        RuleFor(b => b.Id)
            .NotEmpty()
            .WithMessage("this field is Required");
    }
}