using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models.Admin
{
    public class AddEmployeeViewModel
    {
        [StringLength(20,MinimumLength =3,ErrorMessage ="Please Enter a Valid Name")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Please provide a valid Name")] 
        public string? Name { get; set; }
         [Required(ErrorMessage = "Enter e-mail")]
         [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please Provide Valid Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
                [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Please provide  Valid Password")]
        public string? userPassword { get; set; }
        [Range(typeof(DateTime),"01-01-1970","01-01-2003",ErrorMessage ="Minimum Age Required is 23")]
        public DateTime DateOfBirth { get; set; }
        public string? Department { get; set; }

    }
}
