using Discord;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot.Converters
{
    public class CommandListConverter : IEntityConverter<CommandList>
    {
        public object ConvertToMessage(CommandList commandList)
        {
            var description = new StringBuilder();

            description.Append("```css\n");
            description.Append($"!{commandList.HelperCommand.Name} {{command}} for command details");
            description.Append(" ");
            description.Append("```");
            
            description.Append("```css\n");
            foreach (var command in commandList.Commands.Where(c => c != commandList.HelperCommand))
            {
                description.Append($"!{command.Name}");
                description.Append(" ");
            }
            description.Append("```");

            var builder = new EmbedBuilder()
                .WithAuthor("Available commands")
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://cdn.discordapp.com/icons/324278390955966464/671a5c63af6541f641c5938485364a38.png")
                .WithColor(Color.DarkRed)
                .WithFooter(footer => footer.Text = $"UnmaxedBot v{commandList.Version}")
                .WithCurrentTimestamp();
            return builder.Build();
        }
    }
}
