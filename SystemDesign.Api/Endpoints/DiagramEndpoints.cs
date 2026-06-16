using System.Security.Claims;
using SystemDesign.Api.Extensions;
using SystemDesign.Application.Common;
using SystemDesign.Application.Diagrams;

namespace SystemDesign.Api.Endpoints;

public static class DiagramEndpoints
{
    public static IEndpointRouteBuilder MapDiagramEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/diagrams")
            .WithTags("Diagrams");

        group.MapGet("/", async (
            string? search,
            ClaimsPrincipal user,
            IDiagramService diagramService,
            CancellationToken cancellationToken) =>
        {
            var userId = user.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Results.Unauthorized();
            }

            // Service/repository scope the result to this user at the DB level.
            var result = await diagramService.GetForUserAsync(userId, search, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : ToProblem(result);
        })
        .WithName("GetUserDiagrams")
        .WithSummary("Gets the current user's diagrams, with optional search by name.")
        .Produces<IReadOnlyList<DiagramDto>>()
        .RequireAuthorization();

        group.MapGet("/{id:guid}", async (
            Guid id,
            IDiagramService diagramService,
            CancellationToken cancellationToken) =>
        {
            var result = await diagramService.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : ToProblem(result);
        })
        .WithName("GetDiagramById")
        .WithSummary("Gets a single diagram by its id.");

        group.MapPost("/", async (
            CreateDiagramRequest request,
            IDiagramService diagramService,
            CancellationToken cancellationToken) =>
        {
            var result = await diagramService.CreateAsync(request, cancellationToken);

            return result.IsSuccess
                ? Results.CreatedAtRoute("GetDiagramById", new { id = result.Value.Id }, result.Value)
                : ToProblem(result);
        })
        .WithName("CreateDiagram")
        .WithSummary("Creates a new diagram.");

        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateDiagramRequest request,
            IDiagramService diagramService,
            CancellationToken cancellationToken) =>
        {
            var result = await diagramService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : ToProblem(result);
        })
        .WithName("UpdateDiagram")
        .WithSummary("Updates an existing diagram.");

        group.MapDelete("/{id:guid}", async (
            Guid id,
            IDiagramService diagramService,
            CancellationToken cancellationToken) =>
        {
            var result = await diagramService.DeleteAsync(id, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : ToProblem(result);
        })
        .WithName("DeleteDiagram")
        .WithSummary("Deletes a diagram by its id.");

        return app;
    }

    private static IResult ToProblem(Result result) =>
        result.ErrorType switch
        {
            ResultErrorType.NotFound => Results.NotFound(result.Error),
            _ => Results.BadRequest(result.Error)
        };
}
