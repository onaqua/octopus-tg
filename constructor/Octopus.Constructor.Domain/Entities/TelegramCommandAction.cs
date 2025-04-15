using Octopus.Kernel.Domain.Entities;
using System.Text.Json.Serialization;

namespace Octopus.Constructor.Domain;

public class TelegramCommandAction : EntityBase<Guid>
{
    public int Order { get; protected set; } = 0;
    public Guid TelegramCommandId { get; protected set; }

    [JsonIgnore]
    public TelegramCommand? Command { get; protected set; }
}