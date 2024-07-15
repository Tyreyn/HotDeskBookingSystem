namespace SoftwareMind_Intern_Challenge.Services
{
    using SoftwareMind_Intern_ChallengeBL.Operations;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Employee service.
    /// </summary>
    /// <param name="employeeOperations">
    /// Available operations for employee model.
    /// </param>
    public class EmployeeService(EmployeeOperations employeeOperations)
    {
        private readonly EmployeeOperations employeeOperations = employeeOperations;

        /// <summary>
        /// Get employee and check credentials.
        /// </summary>
        /// <param name="email">
        /// Employee email to get.
        /// </param>
        /// <param name="password">
        /// Employee password to get.
        /// </param>
        /// <returns>
        /// Employee object.
        /// </returns>
        public Employee? GetEmployeeAndCheckCredentials(string email, string password)
        {
            Employee? employee = this.employeeOperations.GetEmployeeByEmail(email);
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

        /// <summary>
        /// Add new employee.
        /// </summary>
        /// <param name="email">
        /// New employee email.
        /// </param>
        /// <param name="password">
        /// New employee password.
        /// </param>
        /// <returns>
        /// True, if employee added correctly, otherwise false.
        /// </returns>
        public bool AddNewEmployee(string email, string password)
        {
            Employee? employeeToCheck = this.employeeOperations.GetEmployeeByEmail(email);
            if (employeeToCheck == null)
            {
                this.employeeOperations.AddNewEmployee(
                    new Employee
                    {
                        Email = email,
                        Password = password,
                        Role = "user",
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
