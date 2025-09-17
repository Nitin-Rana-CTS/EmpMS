using backend.Models.Dtos.Admin;
using backend.Models.Dtos.Employee;

namespace backend.Services
{
    public interface IAdminService
    {
        Task<bool> EditEmployee(EmployeeEditRequestDto request);

        Task<List<EmployeeDto>> GetAllEmployees();

        Task<bool> DeleteEmployee(string email);
    }
}
