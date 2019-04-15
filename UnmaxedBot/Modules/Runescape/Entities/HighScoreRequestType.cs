namespace UnmaxedBot.Modules.Runescape.Entities
{
    public class HighScoreRequestType
    {
        public static HighScoreRequestType Clues = new HighScoreRequestType("Clues");

        private string _typeIdentifier;

        private HighScoreRequestType(string typeIdentifier)
        {
            _typeIdentifier = typeIdentifier;
        }
    }
}
