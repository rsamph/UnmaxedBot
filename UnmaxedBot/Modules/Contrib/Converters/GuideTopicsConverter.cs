using Discord;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Contrib.Entities;

namespace UnmaxedBot.Modules.Contrib.Converters
{
    public class GuideTopicsConverter : IEntityConverter<GuideTopics>
    {
        public object ConvertToResponse(GuideTopics result)
        {
            var description = new StringBuilder();
            description.Append("```css\n");
            foreach (var topic in result.Topics)
            {
                description.Append($"{topic} ");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Contributed guides")
                .WithDescription(description.ToString());
        }
    }
}
