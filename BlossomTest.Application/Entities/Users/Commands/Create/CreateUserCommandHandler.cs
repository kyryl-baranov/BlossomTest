using System.Text;
using BlossomTest.Domain.Entities;
using BlossomTest.Domain.Entities.Security;

namespace BlossomTest.Application.Entities.Users.Commands.Create;

internal class CreateUserCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IPasswordHasher passwordHasher, IEnumerable<IValidator<CreateUserCommand>> validators)
    : BaseRequestHandler<CreateUserCommand, int>(validators)
{
    private static readonly CompositeFormat notFoundErrorMessage = CompositeFormat.Parse(GeneralErrors.NotFoundErrorMessage);

    private static readonly CompositeFormat generalErrorMessage = CompositeFormat.Parse(GeneralErrors.GeneralCreateErrorMessage);

    protected override async Task<Result<int>> HandleRequest(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Address? address = null;
        string? passwordHash = null;

        if (request.Address is not null)
        {
            address = new Address(request.Address.City, request.Address.Street, request.Address.PostalCode);
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            passwordHash = passwordHasher.HashPassword(request.Password);
        }

        Result<User> userResult = User.Create(request.FirstName, request.LastName, request.Email, passwordHash, address,
            (Gender?)request.Gender);

        if (!userResult.IsSuccess)
        {
            return Result<int>.Failure(userResult.Errors.ToArray());
        }

        Role? role = await applicationUnitOfWork.Roles
            .SingleOrDefaultAsync(x => x.Id == (int)Roles.Member, cancellationToken).ConfigureAwait(false);

        if (role is null)
        {
            return Result<int>.Failure(string.Format(CultureInfo.InvariantCulture, notFoundErrorMessage,
                nameof(Role)));
        }

        userResult.Value!.AddRole(role);

        await applicationUnitOfWork.Users.AddAsync(userResult.Value!, cancellationToken).ConfigureAwait(false);

        Result result = await applicationUnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return result.IsSuccess
            ? Result<int>.Success(userResult.Value!.Id)
            : Result<int>.Failure(string.Format(CultureInfo.InvariantCulture, generalErrorMessage, nameof(User)));
    }
}
