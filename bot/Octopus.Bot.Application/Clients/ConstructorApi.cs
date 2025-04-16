using Octopus.Constructor.Shared;
using Refit;

namespace Octopus.Bot.Application.Clients;

public interface IConstructorApi
{
    [Get("/templates/{id}")]
    public Task<ApiResponse<TemplateDto>> GetTemplateAsync(Guid id, CancellationToken cancellationToken);
}