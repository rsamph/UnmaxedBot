namespace UnmaxedBot.Entities
{
    public abstract class GenericEntityConverter<T1, T2> where T1 : IEntity
    {
        public abstract T2 ToDiscordMessage(T1 entity);
    }
}
