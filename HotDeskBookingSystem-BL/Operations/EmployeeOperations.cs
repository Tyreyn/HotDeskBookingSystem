namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    using Microsoft.EntityFrameworkCore;
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Employee operations class.
    /// </summary>
    /// <param name="hotDeskBookingSystemContexts">
    /// Database context.
    /// </param>
    public class EmployeeOperations(HotDeskBookingSystemContext hotDeskBookingSystemContexts) : IEmployeeOperations
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
        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await this.hotDeskBookingSystemContexts.Employees
                .SingleOrDefaultAsync(employee => employee.Email == email);
        }

        /// <summary>
        /// Add new employee.
        /// </summary>
        /// <param name="newEmployee">
        /// New Employee object to add.
        /// </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddNewEmployee(Employee newEmployee)
        {
            await this.hotDeskBookingSystemContexts.Employees.AddAsync(newEmployee);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }
    }
}
