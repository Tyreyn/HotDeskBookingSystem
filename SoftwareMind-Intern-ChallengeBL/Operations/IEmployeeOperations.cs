using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    public interface IEmployeeOperations
    {
        Task AddNewEmployee(Employee newEmployee);
        Task<Employee?> GetEmployeeByEmail(string email);
    }
}