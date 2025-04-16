using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Kernel.Application;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

public static partial class CreateTemplateCommandAction
{
    public sealed class Endpoint(ISender sender) : ResultEndpoint<Request, TelegramAction>
    {
        public override void Configure()
        {
            Post("/templates/{TemplateId:guid}/commands/{CommandId:guid}/actions");
            Tags("Templates");
            Summary(s =>
            {
                s.Summary = "Create telegram buttons";
                s.Description = "Create telegram buttons";
            });
            Validator<Validator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken ct) => await SendResultAsync(await sender.Send(request, ct), ct);
    }
}