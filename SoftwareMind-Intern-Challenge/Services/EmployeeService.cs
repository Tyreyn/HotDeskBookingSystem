using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.Data;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_Challenge.Services
{
    public class EmployeeService(EmployeeOperations employeeOperations)
    {
        private readonly EmployeeOperations employeeOperations = employeeOperations;

        public async Task<Employee?> GetEmployeeAndCheckCredentials(string email, string password)
        {
            Employee employee = this.employeeOperations.GetEmployeeByEmail(email);
            if (employee == null)
            {
                return null;
            }
            else if (employee.Email == email && employee.Password == password)
            {
                return employee;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddNewEmployee(string email, string password)
        {
            Employee employeeToCheck = this.employeeOperations.GetEmployeeByEmail(email);
            if (employeeToCheck == null)
            {
                this.employeeOperations.AddNewEmployee(
                    new Employee
                    {
                        Email = email,
                        Password = password,
                        Role = "user"
                    });
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
