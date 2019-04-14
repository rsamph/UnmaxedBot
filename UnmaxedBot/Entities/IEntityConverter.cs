namespace UnmaxedBot.Entities
{
    public interface IEntityConverter<T> where T : IEntity
    {
        object ConvertToMessage(T entity);
    }
}
