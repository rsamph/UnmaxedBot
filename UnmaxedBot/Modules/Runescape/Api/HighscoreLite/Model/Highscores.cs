using System.Collections.Generic;

namespace UnmaxedBot.Modules.Runescape.Api.HighscoreLite.Model
{
    public class Highscores
    {
        public IList<HighscoreSkill> Skills { get; set; }
        public IList<HighscoreActivity> Activities { get; set; }

        public Highscores()
        {
            Skills = new List<HighscoreSkill>();
            Activities = new List<HighscoreActivity>();
        }
    }
}
