using backend.Data;
using backend.Models.Dtos.Employee;
using backend.Repositories;
using backend.Data;
using backend.Models;
using backend.Models.Dtos.Employee;
using backend.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace backend.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> GetProfile(string email)
        {
            var employee = await _employeeRepository.GetByEmailAsync(email);
            if (employee == null) return null;

            var emp = new EmployeeDto();
            emp.Name = employee.Name;
            emp.Email = email;
            emp.Address = employee.Address;
            emp.Phone = employee.Phone;

            return emp;
        }

        public async Task<bool> UpdateProfile(string email, EmployeeUpdateMyProfileRequestDto request)
        {
            var employee = await _employeeRepository.GetByEmailAsync(email);
            
            if (employee == null) return false;

            employee.Address = request.Address;
            employee.Phone = request.Phone;

            var isUpdated = await _employeeRepository.UpdateAsync(employee);
            if (!isUpdated) return false;
            return true;
        }

    }
}
