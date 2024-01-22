namespace Todo.Data.Models
{
    public class TodoEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime? DueDate { get; set; }
        public long UserId { get; set; }

        // Navigation properties
        public UserEntity User { get; set; } = null!;
    }
}
