namespace UnmaxedBot.Modules.Contrib.Entities
{
    public class Alias : Contrib
    {
        public string Name { get; set; }
        public string AlsoKnownAs { get; set; }

        public override string NaturalKey => Name + AlsoKnownAs;
    }
}
