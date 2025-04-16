using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;

namespace Octopus.Kernel.Application;

public abstract class ResultEndpointWithoutRequest<TResponse> : EndpointWithoutRequest<TResponse>
{
    protected async Task SendResultAsync(Result<TResponse> result, CancellationToken ct)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                await SendAsync(result.Value, cancellation: ct);
                break;
            case ResultStatus.Created:
                await SendAsync(result.Value, 201, ct);
                break;
            case ResultStatus.Invalid:
                HandleValidationErrors(result.ValidationErrors);
                break;
            case ResultStatus.NotFound:
                await SendNotFoundAsync(ct);
                break;
            default:
                HandleUnhandledErrors(result);
                break;
        }
    }

    private void HandleValidationErrors(
        IEnumerable<ValidationError> errors)
    {
        ValidationFailures.AddRange(errors.Select(e =>
            new ValidationFailure(e.Identifier, e.ErrorMessage)));

        ThrowIfAnyErrors();
    }

    private void HandleUnhandledErrors(
        Result<TResponse> result)
    {
        AddError(string.Join("\n", result.Errors));
        ThrowIfAnyErrors();
    }
}