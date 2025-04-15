using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.Constructor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
