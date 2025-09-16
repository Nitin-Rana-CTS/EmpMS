using backend.Models.Dtos.Employee;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public interface IEmployeeService
    {
        public Task<EmployeeDto> GetProfile(string email);
        public Task<bool> UpdateProfile(string email, EmployeeUpdateMyProfileRequestDto request);

    }
}