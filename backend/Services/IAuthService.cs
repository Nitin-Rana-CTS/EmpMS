using backend.Models.Dtos.Admin;
using backend.Models.Dtos.Employee;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> EmployeeRegister(EmployeeRegisterRequestDto request);
        Task<EmployeeLoginResponseDto> EmployeeLogin(EmployeeLoginRequestDto request);

        Task<AdminLoginResponseDto> AdminLogin(AdminLoginRequestDto request);


    }
}
