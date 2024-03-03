namespace ToDoListProject.DTOs
{
    public class CourseAndItsHomeWorksDTO
    {
        public string? Name { get; set; }
        public List<string>? WorksNames { get; set; } = new List<string>();
    }
}
