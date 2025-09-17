using backend.Models.Entities;

namespace backend.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByEmailAsync(string email);

        //Task<Employee> GetByIdAsync(int id);
        //Task<List<Employee>> GetAllAsync();
        Task<bool> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);

        Task<List<Employee>> GetAllAsync();

        Task<bool> DeleteAsync(int id);

    }
}
