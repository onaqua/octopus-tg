using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Kernel.Application;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplate;

public static partial class CreateTemplate
{
    public sealed class Endpoint(ISender sender) : ResultEndpoint<Request, Template>
    {
        public override void Configure()
        {
            Post("/templates");
            Tags("Templates");
            Summary(s =>
            {
                s.Summary = "Create template";
                s.Description = "Create template";
            });
            Validator<Validator>();
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken ct) => await SendResultAsync(await sender.Send(request, ct), ct);
    }
}