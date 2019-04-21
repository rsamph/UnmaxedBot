using Discord;
using Discord.Commands;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Core.Extensions;
using UnmaxedBot.Modules.Bot.Entities;

namespace UnmaxedBot.Modules.Bot.Converters
{
    public class CommandDetailsConverter : IEntityConverter<CommandDetails>
    {
        public object ConvertToResponse(CommandDetails details)
        {
            var title = new StringBuilder();
            title.Append($"Command !{details.Command.Name}");
            if (details.Command.IsAdminCommand())
                title.Append($" (admin)");
            
            var description = new StringBuilder();
            description.Append(details.Command.Remarks);
            description.Append("```css\n");
            description.Append($"!{details.Command.Name} ");
            
            ParameterInfo previousParameter = null;
            foreach (var parameter in details.Command.Parameters)
            {
                // Two subsequent string parameters are separated by a pipe
                if (parameter.Type == typeof(string) && previousParameter != null && previousParameter.Type == typeof(string))
                    description.Append("| ");

                if (parameter.IsOptional)
                    description.Append("{" + parameter.Name + "} ");
                else
                    description.Append($"<{parameter.Name}> ");
                previousParameter = parameter;
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor(title.ToString())
                .WithDescription(description.ToString());
        }
    }
}
