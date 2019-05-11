using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using UnmaxedBot.Core;
using UnmaxedBot.Modules.Runescape.Entities;

namespace UnmaxedBot.Modules.Runescape.Converters
{
    public class SongOfSerenResultConverter : IEntityConverter<SongOfSerenResult>
    {
        public object ConvertToResponse(SongOfSerenResult result)
        {
            var description = new StringBuilder();

            var activeSong = result.TimeTable.ActiveSong;
            var nextSong = result.TimeTable.NextSong;
            var timeRemaining = nextSong.Time.Subtract(SongOfSerenTimeTable.GameTime).Minutes;

            description.Append($"Active: {activeSong.SkillingGroup}");
            description.Append($" ({timeRemaining} minutes remaining)");
            var activeSkills = GetSkillsForSkillingGroup(activeSong.SkillingGroup);
            description.Append("```css\n");
            description.Append(string.Join(", ", activeSkills));
            description.Append("```");

            description.Append("Perks:\n");
            var activePerks = GetPerksForSkillingGroup(activeSong.SkillingGroup);
            foreach (var perk in activePerks)
                description.Append($"{perk}\n");

            description.Append($"\nNext: {nextSong.SkillingGroup}");
            var nextSkills = GetSkillsForSkillingGroup(nextSong.SkillingGroup);
            description.Append("```css\n");
            description.Append(string.Join(", ", nextSkills));
            description.Append("```");

            description.Append("Perks:\n");
            var nextPerks = GetPerksForSkillingGroup(nextSong.SkillingGroup);
            foreach (var perk in nextPerks)
                description.Append($"{perk}\n");

            return new EmbedBuilder()
                .WithAuthor("Song of seren")
                .WithDescription(description.ToString())
                .WithThumbnailUrl("https://vignette.wikia.nocookie.net/runescape2/images/5/50/Seren_%28with_Eluned%29_chathead.png/revision/latest?cb=20151030153838");
        }

        private IEnumerable<Skill> GetSkillsForSkillingGroup(SkillingGroup group)
        {
            if (group == SkillingGroup.Artisan)
            {
                return new[] { Skill.Cooking, Skill.Construction, Skill.Crafting, Skill.Firemaking, Skill.Fletching, Skill.Herblore, Skill.Runecrafting, Skill.Smithing };
            }
            if (group == SkillingGroup.Combat)
            {
                return new[] { Skill.Attack, Skill.Strength, Skill.Defense, Skill.Ranged, Skill.Prayer, Skill.Magic, Skill.Consitution, Skill.Summoning };
            }
            if (group == SkillingGroup.Gathering)
            {
                return new[] { Skill.Mining, Skill.Fishing, Skill.Woodcutting, Skill.Farming, Skill.Hunter, Skill.Divination };
            }
            if (group == SkillingGroup.Support)
            {
                return new[] { Skill.Agility, Skill.Thieving, Skill.Slayer, Skill.Dungeoneering, Skill.Invention };
            }

            throw new Exception($"No skills defined for skilling group {group}");
        }

        private IEnumerable<string> GetPerksForSkillingGroup(SkillingGroup group)
        {
            if (group == SkillingGroup.Support)
            {
                return new[] {
                    "No XP penalty for dying in Dungeoneering",
                    "More loot and chance of getting totems whilst safecracking",
                    "All Slayer tasks act as if you have VIP tickets"
                };
            }
            if (group == SkillingGroup.Combat)
            {
                return new[] {
                    "+1 to all charm drops",
                    "Decreased cost of instances",
                    "1 auto resurrection to full health per run in Elite Dungeons"
                };
            }
            if (group == SkillingGroup.Gathering)
            {
                return new[] {
                    "10% chance not to deplete Uncharted Isles resources",
                    "Training hotspots in Hall of Memories don’t move as often",
                    "10% more beans from selling items in the Player Owned Farm"
                };
            }
            if (group == SkillingGroup.Artisan)
            {
                return new[] {
                    "Reduced chance of burning food",
                    "2.5% chance to save a secondary when making potions",
                    "Increases node spawn and double rewards in the RuneSpan"
                };
            }

            throw new Exception($"No perks defined for skilling group {group}");
        }
    }
}
