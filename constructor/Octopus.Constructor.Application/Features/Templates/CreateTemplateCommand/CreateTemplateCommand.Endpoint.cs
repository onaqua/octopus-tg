using MediatR;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Application.Features.Templates;

public partial class CreateTemplateCommand
{
    public sealed class Endpoint(ISender sender) : ResultEndpoint<Request, TelegramCommand>
    {
        public override void Configure()
        {
            Post("/templates/{TemplateId}/commands");
            Tags("Templates");
            Summary(s =>
            {
                s.Summary = "Add telegram command to template";
                s.Description = "Add telegram command to template";
            });
            Validator<Validator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken ct) => await SendResultAsync(await sender.Send(request, ct), ct);
    }
}