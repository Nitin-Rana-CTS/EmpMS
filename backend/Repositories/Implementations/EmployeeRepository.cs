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

            return await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<bool> UpdateAsync(Employee employee)
        {
            _appDbContext.Employees.Update(employee);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddAsync(Employee employee)
        {
            await _appDbContext.Employees.AddAsync(employee);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null) return false;
            _appDbContext.Employees.Remove(employee);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
