using Discord;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot.Converters
{
    public class CommandDetailsConverter : IEntityConverter<CommandDetails>
    {
        public object ConvertToResponse(CommandDetails details)
        {
            var description = new StringBuilder();
            description.Append(details.Command.Remarks);
            description.Append("```css\n");
            description.Append($"!{details.Command.Name} ");
            foreach (var parameter in details.Command.Parameters)
            {
                if (parameter.IsOptional)
                    description.Append("{" + parameter.Name + "} ");
                else
                    description.Append($"<{parameter.Name}> ");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor($"Command !{details.Command.Name}")
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://cdn.discordapp.com/icons/324278390955966464/671a5c63af6541f641c5938485364a38.png")
                .WithColor(Color.DarkRed);
        }
    }
}
