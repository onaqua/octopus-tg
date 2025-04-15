using Octopus.Constructor.Domain;
using Octopus.Kernel.Infrastructure;

namespace Octopus.Constructor.Infrastructure.Templates.Specifications;

internal sealed class GetTemplateByIdSpecification : SpecificationBase<Template>
{
    public GetTemplateByIdSpecification(Guid id) : base(template => template.Id.Equals(id))
    {
    }
}