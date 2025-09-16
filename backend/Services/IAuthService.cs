using backend.Models.Dtos.Employee;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<EmployeeLoginResponseDto> EmployeeLogin(EmployeeLoginRequestDto request);
        Task<IdentityResult> EmployeeRegister(EmployeeRegisterRequestDto request);

    }
}
