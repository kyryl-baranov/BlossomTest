using BlossomTest.Domain.Entities;

namespace BlossomTest.Application.Entities.Applications.Queries.Get;

internal class GetApplicationQueryHandler(IApplicationUnitOfWork applicationUnitOfWork, IEnumerable<IValidator<GetApplicationQuery>> validators)
    : BaseRequestHandler<GetApplicationQuery, ApplicationResponse>(validators)
{
    protected override async Task<Result<ApplicationResponse>> HandleRequest(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        UserApplication? application = await applicationUnitOfWork.Applications.FindAsync(keyValues: [request.Id], cancellationToken).ConfigureAwait(false);

        return application is null ? Result<ApplicationResponse>.Failure("Application Not Found.") : Result<ApplicationResponse>.Success(application.ToResponse());
    }
}
