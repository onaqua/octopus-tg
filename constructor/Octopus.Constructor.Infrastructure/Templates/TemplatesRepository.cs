using Microsoft.EntityFrameworkCore;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;
using Octopus.Constructor.Infrastructure.Templates.Specifications;
using Octopus.Kernel.Infrastructure;

namespace Octopus.Constructor.Infrastructure.Repositories;

public class TemplatesRepository(DatabaseContext context) : RepositoryBase<Template>(context.Templates), ITemplateRepository
{
    public async Task AddAsync(Template entity, CancellationToken cancellationToken = default) =>
        await Set.AddAsync(entity, cancellationToken);

    public async Task<Template?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await UseSpecification(new GetTemplateByIdSpecification(id))
            .SingleOrDefaultAsync(cancellationToken);

    public Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        Set.AnyAsync(x => x.Id.Equals(id), cancellationToken);
}