using Discord;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Extensions;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot.Converters
{
    public class CommandListConverter : IEntityConverter<CommandList>
    {
        public object ConvertToResponse(CommandList commandList)
        {
            var description = new StringBuilder();

            description.Append("```css\n");
            foreach (var command in commandList.Commands.Where(c => c != commandList.HelperCommand && !c.IsAdminCommand()))
            {
                description.Append($"!{command.Name}");
                description.Append(" ");
            }
            description.Append("```");

            description.Append("\nAdministrators:");
            description.Append("```css\n");
            foreach (var command in commandList.Commands.Where(c => c.IsAdminCommand()))
            {
                description.Append($"!{command.Name}");
                description.Append(" ");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Available commands")
                .WithDescription(description.ToString());
        }
    }
}
