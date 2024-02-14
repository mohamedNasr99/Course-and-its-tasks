using System.ComponentModel.DataAnnotations;

namespace ToDoListProject.DTOs
{
    public class HomeWorkDTO
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public int CourseId { get; set; }
    }
}
