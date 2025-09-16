using backend.Models.Dtos.Employee;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost]
        [Route("employeeRegister")]
        public async Task<IActionResult> EmployeeRegister([FromBody] EmployeeRegisterRequestDto request)
        {
            var result = await _authService.EmployeeRegister(request);
            if (result.Succeeded)
            {
                return Ok("Employee registered successfully");
            }
            return BadRequest("Employee registration failed");
        }

        [HttpPost]
        [Route("employeeLogin")]
        public async Task<IActionResult> EmployeeLogin([FromBody] EmployeeLoginRequestDto request)
        {
            var result = await _authService.EmployeeLogin(request);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid email or password");
        }
    }
}