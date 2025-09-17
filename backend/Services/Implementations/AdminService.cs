using backend.Models.Dtos.Admin;
using backend.Models.Dtos.Employee;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Identity;

namespace backend.Services.Implementations
{
    public class AdminService : IAdminService
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;

        public AdminService(UserManager<IdentityUser> userManager, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> EditEmployee(EmployeeEditRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.oldEmail);
            if (user == null)
            {
                return false; // User not found
            }

            if (request.newEmail != request.oldEmail)
            {
                // first check if newEmail is already taken by another employee/admin
                var existingUser = await _userManager.FindByEmailAsync(request.newEmail);
                if (existingUser != null) {
                    return false; // newEmail already taken
                }
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.newEmail);
                var emailChangeResult = await _userManager.ChangeEmailAsync(user, request.newEmail, token);
                if (!emailChangeResult.Succeeded)
                {
                    return false; // Email change failed
                }

                var userNameChangeResult = await _userManager.SetUserNameAsync(user, request.newEmail);
                if (!userNameChangeResult.Succeeded)
                {
                    return false; // Username change failed
                }
            }

            var employee = await _employeeRepository.GetByEmailAsync(request.oldEmail);
            if (employee == null) {
                return false; // Employee not found
            }
            employee.Name = request.Name;
            employee.Email = request.newEmail;
            employee.Address = request.Address;
            employee.Phone = request.Phone;

            var employeeUpdated = await _employeeRepository.UpdateAsync(employee);

            if (!employeeUpdated) {
                return false; // Employee update failed
            }
            return true;
        }
    
        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();

        var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Name = e.Name,
                Email = e.Email,
                Address = e.Address,
                Phone = e.Phone
            }).ToList();

            return employeeDtos;
        }

        public async Task<bool> DeleteEmployee(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false; // User not found
            }



            // Remove user from roles first
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, role);
                if (!removeRoleResult.Succeeded) return false;
            }






            var employee = await _employeeRepository.GetByEmailAsync(email);
            if (employee == null)
            {
                return false; // Employee not found
            }
            var deleteEmployeeResult = await _employeeRepository.DeleteAsync(employee.Id);
            if (!deleteEmployeeResult)
            {
                return false; // Employee deletion failed
            }
            var deleteUserResult = await _userManager.DeleteAsync(user);
            if (!deleteUserResult.Succeeded)
            {
                return false; // employee info deleted  but login remains (ISSUE TO BE SOLVED)
            }

            //// Removing roles
            //var roles = await _userManager.GetRolesAsync(user);
            //if (roles.Any())
            //{
            //    var roleResult = await _userManager.RemoveFromRolesAsync(user, roles);
            //    if (!roleResult.Succeeded) return false; // employee info deleted  but roles remains (ISSUE TO BE SOLVED)
            //}

            return true;
        }
    }
}
