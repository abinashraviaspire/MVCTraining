namespace FirstApp.Models.Admin
{
    public class AddEmployeeViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string userPassword { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }

    }
}
