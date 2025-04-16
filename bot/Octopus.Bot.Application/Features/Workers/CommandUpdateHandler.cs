using Octopus.Constructor.Shared;
using Octopus.Kernel.Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Octopus.Bot.Application.Features.Workers;

public class CommandUpdateHandler(TemplateDto templateDto) : IUpdateHandler
{
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } userMessage)
        {
            return;
        }

        var command = templateDto
            .Commands
            .FirstOrDefault(x => x.Trigger == update.Message.Text);

        if (command is null)
        {
            return;
        }

        foreach (var action in command.Actions)
        {
            if (action.ActionType is TelegramActionType.SendTextMessages && action.Messages is not null)
            {
                foreach (var telegramMessage in action.Messages)
                {
                    await botClient
                        .SendMessage(userMessage.Chat.Id, telegramMessage.Text, cancellationToken: cancellationToken);
                }
            }

            if (action.ActionType is TelegramActionType.SendDocuments && action.Documents is not null)
            {
                foreach (var telegramDocument in action.Documents)
                {
                    await using var stream = new MemoryStream(telegramDocument.Payload);

                    await botClient
                        .SendDocument(userMessage.Chat.Id, InputFile.FromStream(stream), cancellationToken: cancellationToken);
                }
            }

            if (action.ActionType is TelegramActionType.SendButtons && action.Buttons is not null)
            {       
                var replyKeyboardButton = new ReplyKeyboardMarkup
                {
                    Keyboard = [action.Buttons.Select(button => new KeyboardButton(button.Text))]
                };

                foreach (var telegramButton in action.Buttons)
                {
                    await botClient
                        .SendMessage(userMessage.Chat.Id, "", replyMarkup: replyKeyboardButton, cancellationToken: cancellationToken);
                }
            }
        }
    }
}