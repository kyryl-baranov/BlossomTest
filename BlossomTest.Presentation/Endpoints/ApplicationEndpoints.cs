using BlossomTest.Domain.Enums;
using BlossomTest.Presentation.Configuration;

namespace BlossomTest.Presentation.Endpoints;

internal static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints(this WebApplication app)
    {
        app.MapPost("/applications", CreateApplication)
            .WithOpenApi()
            .WithSummary("Creates a new application")
            .WithDescription("Creates a new application with trading account.")
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Created, "The ID of the created application", typeof(int)))
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, "Invalid input parameters"))
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request", typeof(ProblemDetails)))
            .RequireAuthorization(Permissions.CanRead.ToString()); 
        
        app.MapPatch("/applications", UpdateApplicationUpdatesInfo)
            .WithOpenApi()
            .WithSummary("Updates applicant info")
            .WithDescription("Updates applicant tfn.")
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Created, "The ID of the application", typeof(int)))
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, "Invalid input parameters"))
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request", typeof(ProblemDetails)))
            .RequireAuthorization(Permissions.CanRead.ToString());

        app.MapGet("/applications/{id:int}", GetApplication)
            .WithOpenApi()
            .WithSummary("Gets a application by ID")
            .WithDescription("Gets a application with the specified ID.")
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, "The application was found", typeof(ApplicationResponse)))
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, "The application was not found"))
            .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request", typeof(ProblemDetails)))
            .RequireAuthorization(Permissions.CanRead.ToString());
    }

    private static async Task<IResult> GetApplication(ISender sender, int id)
    {
        Result<ApplicationResponse> result = await sender.Send(new GetApplicationQuery(id)).ConfigureAwait(false);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.NotFound();
    }

    private static async Task<IResult> UpdateApplicationUpdatesInfo(ISender sender, UpdateApplicationCommand command)
    {
        Result result = await sender.SendWithRetryAsync(command).ConfigureAwait(false);

        return result.IsSuccess
            ? Results.NoContent()
            : Results.BadRequest(string.Join(',', result.Errors.Select(x => x.Message)));
    }

    private static async Task<IResult> CreateApplication(ISender sender, CreateApplicationCommand command)
    {
        Result<int> result = await sender.Send(command).ConfigureAwait(false);

        return !result.IsSuccess
            ? Results.BadRequest(string.Join(',', result.Errors.Select(x => x.Message)))
            : Results.Created($"/applications/{result.Value}", result);
    }
}
