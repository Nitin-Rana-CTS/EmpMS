using backend.Models.Dtos.Admin;
using backend.Models.Dtos.Employee;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;
        public AdminController(IAuthService authService, IAdminService adminService)
        {
            _authService = authService;
            _adminService = adminService;
        }

        [HttpPost]
        [Route("employees")]
        [Authorize(Roles = "Admin")]
        public  async Task<IActionResult> CreateEmployee([FromBody] EmployeeRegisterRequestDto request)
        {
            var result = await _authService.EmployeeRegister(request);
            if (result.Succeeded)
            {
                return Ok("Employee registered successfully");
            }
            return BadRequest("Employee registration failed");
        }

        [HttpGet]
        [Route("employees")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _adminService.GetAllEmployees();
            return Ok(employees);
        }


        [HttpPut]
        [Route("employees")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployee([FromBody] EmployeeEditRequestDto request)
        {
            var result = await _adminService.EditEmployee(request);
            if (result)
            {
                return Ok("Employee updated successfully");
            }
            return BadRequest("Employee update failed");
        }

        [HttpDelete]
        [Route("employees/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] string email)
        {
            var result = await _adminService.DeleteEmployee(email);
            if (result)
            {
                return Ok("Employee deleted successfully");
            }
            return BadRequest("Employee deletion failed");
        }

    }
}
