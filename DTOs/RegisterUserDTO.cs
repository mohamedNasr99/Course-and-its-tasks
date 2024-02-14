using System.ComponentModel.DataAnnotations;

namespace ToDoListProject.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = " Password And Confirm Password Are Not Same")]
        public string? ConfirmPassword { get; set; }
    }
}
