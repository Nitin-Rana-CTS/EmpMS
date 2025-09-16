namespace backend.Models.Dtos.Employee
{
    public class EmployeeRegisterRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
