using Discord;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Entities;
using UnmaxedBot.Modules.Runescape.Extensions;

namespace UnmaxedBot.Modules.Runescape.Converters
{
    public class TopSkillsResultConverter : IEntityConverter<HighscoreResult>
    {
        public object ConvertToResponse(HighscoreResult highscore)
        {
            var topSkills = highscore.Skills
                .Where(s => s.SkillType != HighscoreSkillType.Overall)
                .Where(s => s.Experience > 0)
                .OrderByDescending(s => s.Experience)
                .ThenBy(s => s.Rank)
                .Take(6);
            return ToEmbed(highscore, topSkills);
        }

        private EmbedBuilder ToEmbed(HighscoreResult highscore, IEnumerable<HighscoreSkill> skills)
        {
            var description = new StringBuilder();
            description.Append("```css\n");
            foreach (var skill in skills)
            {
                description.Append(skill.SkillType);
                description.Append(" : ");
                description.Append(skill.Experience.AsShorthandValue());
                description.Append($" ({skill.Rank.AsReadableRank()}) ");
                description.Append("\n");
            }
            description.Append("```");

            return new EmbedBuilder()
                .WithAuthor("Top skills for " + highscore.PlayerName)
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS29SiLHdiXw55R2XX-wAjrNDoO87iWzKS9v24yv54s43mDg4UVHA");
        }
    }
}
