namespace Todo.Shared.Data
{
    public interface IAuditableEntity<T> : IEntity<T>
    {
        T CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        T LastModifiedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
