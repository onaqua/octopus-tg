using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Infrastructure.Templates.Specifications;

internal sealed class GetTemplateByIdSpecification : SpecificationBase<Template>
{
    public GetTemplateByIdSpecification(Guid id) : base(template => template.Id.Equals(id))
    {
    }
}