namespace UnmaxedBot.Modules.Contrib.Entities
{
    public interface IContrib
    {
        int ContribKey { get; set; }
        string NaturalKey { get; }
        Contributor Contributor { get; set; }
    }
}
