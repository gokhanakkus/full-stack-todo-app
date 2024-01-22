namespace Todo.Shared.Data
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
