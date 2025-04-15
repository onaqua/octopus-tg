using MediatR;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

public static partial class CreateTemplateCommandSendButtonsAction
{
    public sealed class Endpoint(ISender sender) : ResultEndpoint<Request, TelegramSendButtonsAction>
    {
        public override void Configure()
        {
            Post("/templates/{TemplateId}/commands/actions/buttons");
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