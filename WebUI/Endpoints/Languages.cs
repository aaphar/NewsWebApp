using Application.Language.Commands.CreateLanguage;
using Application.Language.Commands.DeleteLanguage;
using Application.Language.Commands.UpdateLanguage;
using Application.Language.Queries.GetLanguages;
using Carter;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Endpoints;
public class Languages : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/languages/", async (CreateLanguageCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        //app.MapGet("api/languages", async (ISender sender) =>
        //{
        //    var query = GetLanguageQuery();

        //    var languages = await sender.Send(query);

        //    return Results.Ok(languages)
        //});

        app.MapGet("api/languages/{id}", async (short id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetLanguageQuery(id)));
            }
            catch (LanguageNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("api/languages/{id}", async (short id, [FromBody] UpdateLanguageRequest request, ISender sender) =>
        {
            var command = new UpdateLanguageCommand(
                id,
                request.Name,
                request.LanguageCode);

            await sender.Send(command);

            return Results.NoContent();
        });

        app.MapDelete("api/languages/{id}", async (short id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteLanguageCommand(id));

                return Results.NoContent();
            }
            catch (LanguageNotFoundException e)
            {

                return Results.NotFound(e.Message);
            }
        });
    }
}

