using Octopus.Bot.Domain.Entities;
using Octopus.Constructor.Shared;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Octopus.Bot.Application.Features.Workers;

public class TelegramBotWorker(TelegramBot telegramBot, TemplateDto template)
{
    private readonly TelegramBotClient _telegramBotClient = new TelegramBotClient(telegramBot.Token.Value);

    public void Start()
    {
        _telegramBotClient.StartReceiving(
            updateHandler: new CommandUpdateHandler(template),
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
            });
    }

    public Task StopAsync(CancellationToken cancellationToken = default) => _telegramBotClient.Close(cancellationToken);
}