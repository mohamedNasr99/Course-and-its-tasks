using System.ComponentModel.DataAnnotations;

namespace ToDoListProject.DTOs
{
    public class CourseDTO
    {
        [Required]
        public string? Name { get; set; }
    }
}
