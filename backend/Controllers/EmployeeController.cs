using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult GetMyProfile()
        {
            var userProfile = new
            {
                Id = 1,
                Name = "John Doe",
                Email = "nonefor@now.com"
            };
            return Ok(userProfile);
        }

        [HttpGet]
        [Route("allProfiles")]
        public IActionResult GetAllProfiles()
        {
            var lst = _context.Employees.ToList();
            return Ok(lst);
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<IActionResult> AddUser()
        {
            var user = new Employee();
            user.Name = "Jane Smith 3";
            user.Email = "none";
            user.Address = "none";
            user.Phone = "1024";

            _context.Employees.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User added successfully" });
        }
    }

}
