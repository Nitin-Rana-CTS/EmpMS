using backend.Data;
using backend.Models;
using backend.Models.Dtos.Employee;
using backend.Models.Entities;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("getMyProfile")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyProfile()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return Unauthorized("Email claim not found");
            }
            var profile = await _employeeService.GetProfile(email);
            if (profile == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(profile);
        }

        [HttpPut]
        [Route("updateMyProfile")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] EmployeeUpdateMyProfileRequestDto request)
        {
            //getting mail from claims
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email == null)
            {
                return Unauthorized("Email claim not found");
            }
            var isProfileUpdated = await _employeeService.UpdateProfile(email, request);
            if (!isProfileUpdated)
            {
                return NotFound("Employee not found");
            }
            return Ok("Profile updated successfully");
        }
    }

}
