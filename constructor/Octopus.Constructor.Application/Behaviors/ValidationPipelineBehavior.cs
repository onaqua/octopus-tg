using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Octopus.Constructor.Application.Features.Templates;

// Обновленный ValidationPipelineBehavior
public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failuresTasks = await Task.WhenAll(_validators
            .Select(v => v.ValidateAsync(context)));

        var failures = failuresTasks
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count > 0)
        {
            var errors = failures.ConvertAll(e => new ValidationError
            {
                Identifier = e.PropertyName,
                ErrorMessage = e.ErrorMessage
            });

            if (typeof(TResponse) == typeof(Result))
                return (TResponse)Result.Invalid(errors);

            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                return (TResponse)typeof(TResponse)
                    .GetMethod("Invalid")!
                    .Invoke(null, new object[] { errors })!;
            }
        }

        return await next();
    }
}
