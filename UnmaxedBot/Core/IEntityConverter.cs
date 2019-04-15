namespace UnmaxedBot.Core
{
    public interface IEntityConverter<T> where T : IEntity
    {
        object ConvertToMessage(T entity);
    }
}
