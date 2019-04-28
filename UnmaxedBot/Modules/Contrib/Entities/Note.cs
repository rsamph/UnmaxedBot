namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class Note : Contrib
    {
        public int AssociatedContribKey { get; set; }
        public string Text { get; set; }

        public override string NaturalKey => AssociatedContribKey + Text;

        public override string ToString()
        {
            return $"#{AssociatedContribKey} {Text}";
        }
    }
}