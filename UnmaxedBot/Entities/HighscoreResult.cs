using System.Collections.Generic;
using UnmaxedBot.Entities.Highscores;

namespace UnmaxedBot.Entities
{
    public class HighscoreResult : IEntity
    {
        public string UserName { get; set; }
        public HighScoreRequestType RequestType { get; set; }
        public List<HighscoreSkill> Skills { get; set; }
        public List<HighscoreActivity> Activities { get; set; }
        public string Version => GetType().Assembly.GetName().Version.ToString();

        public HighscoreResult()
        {
            Skills = new List<HighscoreSkill>();
            Activities = new List<HighscoreActivity>();
        }
    }
}
