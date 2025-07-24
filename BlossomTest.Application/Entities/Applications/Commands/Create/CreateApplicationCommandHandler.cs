using BlossomTest.Application.Entities.Applications.Commands.Create;
using BlossomTest.Domain.Entities;
using System.Text;

namespace BlossomTest.Application.Entities.Application.Commands.Create;

internal class CreateApplicationCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IEnumerable<IValidator<CreateApplicationCommand>> validators)
    : BaseRequestHandler<CreateApplicationCommand, int>(validators)
{
    private static readonly CompositeFormat _errorMessage = CompositeFormat.Parse(GeneralErrors.GeneralCreateErrorMessage);

    protected override async Task<Result<int>> HandleRequest(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        Result<UserApplication> application = UserApplication.Create(request.Name, request.ClientAccountId);

        if (!application.IsSuccess)
        {
            return Result<int>.Failure(application.Errors.ToArray());
        }

        await applicationUnitOfWork.Applications.AddAsync(application!, cancellationToken).ConfigureAwait(false);

        Result result = await applicationUnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return result.IsSuccess
            ? Result<int>.Success(application.Value!.Id)
            : Result<int>.Failure(string.Format(CultureInfo.InvariantCulture, _errorMessage, nameof(UserApplication)));
    }
}
