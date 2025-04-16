using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Kernel.Application;

namespace Octopus.Constructor.Application.Features.Templates.GetTemplate;

public static partial class GetTemplate
{
    public class Endpoint(ISender sender) : ResultEndpointWithoutRequest<Template>
    {
        public override void Configure()
        {
            Get("/templates/{TemplateId}");
            Tags("Templates");
            Summary(s =>
            {
                s.Summary = "Get template";
                s.Description = "Get template by id";
            });
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct) => await SendResultAsync(await sender.Send(new Request(Route<Guid>("TemplateId", true)), ct), ct);
    }
}
