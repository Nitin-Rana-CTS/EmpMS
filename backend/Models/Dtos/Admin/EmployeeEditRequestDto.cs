namespace backend.Models.Dtos.Admin
{
    public class EmployeeEditRequestDto
    {
        public string Name { get; set; }
        public string oldEmail { get; set; }
        public string newEmail { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
    }
}