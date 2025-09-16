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


        public async Task<List<Employee>> GetAllAsync() =>
                await _appDbContext.Employees.ToListAsync();


        public async Task AddAsync(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _appDbContext.Employees.Remove(employee);
                await _appDbContext.SaveChangesAsync();
            }
        }

    }
}
