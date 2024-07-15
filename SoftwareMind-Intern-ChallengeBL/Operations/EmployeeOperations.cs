namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Employee operations class.
    /// </summary>
    /// <param name="hotDeskBookingSystemContexts">
    /// Database context.
    /// </param>
    public class EmployeeOperations(HotDeskBookingSystemContext hotDeskBookingSystemContexts)
    {
        /// <summary>
        /// Hot desk booking system context.
        /// </summary>
        private readonly HotDeskBookingSystemContext hotDeskBookingSystemContexts = hotDeskBookingSystemContexts;

        /// <summary>
        /// Get employee by email.
        /// </summary>
        /// <param name="email">
        /// Employee email to get.
        /// </param>
        /// <returns>
        /// Employee object.
        /// </returns>
        public Employee? GetEmployeeByEmail(string email)
        {
            Employee? employee = this.hotDeskBookingSystemContexts.Employees.SingleOrDefault(employee => employee.Email == email);
            return employee;
        }

        /// <summary>
        /// Add new employee.
        /// </summary>
        /// <param name="newEmployee">
        /// New Employee object to add.
        /// </param>
        public void AddNewEmployee(Employee newEmployee)
        {
            this.hotDeskBookingSystemContexts.Employees.Add(newEmployee);
            this.hotDeskBookingSystemContexts.SaveChanges();
        }
    }
}
