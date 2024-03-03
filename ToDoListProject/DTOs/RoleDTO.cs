using System.ComponentModel.DataAnnotations;

namespace ToDoListProject.DTOs
{
    public class RoleDTO
    {
        [Display(Name = "Role")]
        public string? RoleName { get; set; }
    }
}
