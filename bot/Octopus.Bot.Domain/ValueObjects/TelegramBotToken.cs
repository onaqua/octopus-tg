using Ardalis.Result;

namespace Octopus.Bot.Domain.ValueObjects;

public class TelegramBotToken : ValueObject
{
    private TelegramBotToken() { }

    public string Value { get; private set; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<TelegramBotToken> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Invalid(new ValidationError("TelegramBotToken is required"));
        }

        return Result.Created(new TelegramBotToken { Value = value });
    }
}