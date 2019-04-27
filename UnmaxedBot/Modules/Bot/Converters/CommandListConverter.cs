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
            var commandsGroupedByModule = commandList.Commands
                .Where(c => !c.IsAdminCommand())
                .GroupBy(c => c.Module);

            var description = new StringBuilder();

            foreach (var moduleGroup in commandsGroupedByModule)
            {
                description.Append(moduleGroup.Key.Name.Replace("Module", ""));
                description.Append("```css\n");
                foreach (var command in moduleGroup)
                {
                    description.Append($"!{command.Name}");
                    description.Append(" ");
                }
                description.Append("```\n");
            }

            description.Append("Administrators");
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
