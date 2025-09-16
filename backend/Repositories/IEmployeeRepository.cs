using backend.Models.Entities;

namespace backend.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByEmailAsync(string email);

        //Task<Employee> GetByIdAsync(int id);
        //Task<List<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);

        //Task DeleteAsync(int id);

    }
}
