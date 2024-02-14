using System.ComponentModel.DataAnnotations;

namespace ToDoListProject.DTOs
{
    public class LogInDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
