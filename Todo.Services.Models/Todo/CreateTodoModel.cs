using System.ComponentModel.DataAnnotations;

namespace Todo.Services.Models.Todo
{
    public class CreateTodoModel
    {
        [Required, MinLength(1)]
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }

    public class UpdateTodoModel
    {
        [Required, MinLength(1)]
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}
