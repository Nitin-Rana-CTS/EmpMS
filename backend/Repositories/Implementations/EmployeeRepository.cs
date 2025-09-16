using backend.Data;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace backend.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {


        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<Employee> GetByEmailAsync(string email)
        {

              return  await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task UpdateAsync(Employee employee)
        {
            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AddAsync(Employee employee)
        {
            await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
