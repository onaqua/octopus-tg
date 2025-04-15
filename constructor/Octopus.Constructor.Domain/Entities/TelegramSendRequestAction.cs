using Ardalis.Result;

namespace Octopus.Constructor.Domain;

public class TelegramSendRequestAction : TelegramCommandAction
{
    private Dictionary<string, string>? _headers;

    public string Uri { get; private set; } = null!;
    public string Method { get; private set; } = null!;
    public string? Payload { get; private set; }
    public IReadOnlyDictionary<string, string> Headers => _headers ??= [];

    public static Result<TelegramSendRequestAction> Create(int order, string uri, string method, string? payload, IReadOnlyDictionary<string, string> headers)
    {
        if (string.IsNullOrWhiteSpace(uri) || string.IsNullOrWhiteSpace(method))
        {
            return Result.Invalid(new ValidationError("Uri and method is required"));
        }

        return Result.Created(new TelegramSendRequestAction
        {
            Order = order,
            Uri = uri,
            Method = method,
            Payload = payload,
            _headers = headers.ToDictionary(x => x.Key, x => x.Value)
        });
    }
}
