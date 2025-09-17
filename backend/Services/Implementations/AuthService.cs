using backend.Repositories;
using backend.Services;
using backend.Models.Dtos.Employee;
using Microsoft.AspNetCore.Identity;
using backend.Models.Entities;
using backend.Data;
using backend.Models.Dtos.Admin;

namespace backend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly ITokenService _tokenService;
        public AuthService(IEmployeeRepository employeeRepository, UserManager<IdentityUser> userManager, AppDbContext appDbContext, ITokenService tokenService)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> EmployeeRegister(EmployeeRegisterRequestDto request)
        {
            var emp = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(emp, request.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(emp, "Employee");
                if (result.Succeeded)
                {
                    var employee = new Employee();
                    employee.Name = request.Name;
                    employee.Email = request.Email;
                    employee.Address = "none";
                    employee.Phone = "none";

                    var isAdded = await _employeeRepository.AddAsync(employee);
                    if (!isAdded) return null;
                }
            }
            return result;
        }

        public async Task<EmployeeLoginResponseDto> EmployeeLogin(EmployeeLoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isCorrectPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenService.CreateToken(user.Email, roles.ToList());

                    return new EmployeeLoginResponseDto
                    {
                        token = jwtToken
                    };
                }
            }
            return null;
        }
        public async Task<AdminLoginResponseDto> AdminLogin(AdminLoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isCorrectPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _tokenService.CreateToken(user.Email, roles.ToList());

                    return new AdminLoginResponseDto
                    {
                        token = jwtToken
                    };
                }
            }
            return null;
        }
    }
}
