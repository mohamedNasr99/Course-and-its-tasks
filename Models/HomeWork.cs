using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListProject.Models
{
    public class HomeWork
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool? IsFinish { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course?  course { get; set; }
    }
}
