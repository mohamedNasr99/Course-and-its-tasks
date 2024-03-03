using System.ComponentModel.DataAnnotations;

namespace ToDoListProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<HomeWork>? HomeWorks { get; set; }
    }
}
