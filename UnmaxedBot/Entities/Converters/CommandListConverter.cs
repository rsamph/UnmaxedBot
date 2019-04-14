using Discord;
using System.Text;

namespace UnmaxedBot.Entities.Converters
{
    public class CommandListConverter : IEntityConverter<CommandList>
    {
        public object ConvertToMessage(CommandList commandList)
        {
            var description = new StringBuilder();
            foreach (var command in commandList.Commands)
            {
                description.Append("```css\n");
                description.Append(command.Format);
                description.Append(" : ");
                description.Append(command.Description);
                description.Append("\n");
                description.Append("```");
            }

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
