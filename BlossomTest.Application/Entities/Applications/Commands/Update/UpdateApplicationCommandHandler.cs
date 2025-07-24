using BlossomTest.Domain.Entities;

namespace BlossomTest.Application.Entities.Applications.Commands.Update;

internal class UpdateBookRequestHandler(IApplicationUnitOfWork applicationUnitOfWork, IEnumerable<IValidator<UpdateApplicationCommand>> validators)
    : BaseRequestHandler<UpdateApplicationCommand>(validators)
{
    protected override async Task<Result> HandleRequest(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        UserApplication? book = await applicationUnitOfWork.Applications.FindAsync(keyValues: [request.Id], cancellationToken).ConfigureAwait(false);

        if (book is null)
        {
            return Result.Failure("Book Not Found.");
        }

        book.UpdateFromRequest(request);

        return await applicationUnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
