namespace SoftwareMind_Intern_Challenge.Controllers
{
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftwareMind_Intern_Challenge.Services;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Anonymous user controller.
    /// </summary>
    /// <param name="employeeService">
    /// Employee service.
    /// </param>
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AnonymousUserController(EmployeeService employeeService) : ControllerBase
    {
        /// <summary>
        /// Employee service.
        /// </summary>
        private readonly EmployeeService employeeService = employeeService;

        /// <summary>
        /// POST: /AnonymousUser/Login?email=[email]&password=[password].
        /// </summary>
        /// <param name="email">
        /// Employee email.
        /// </param>
        /// <param name="password">
        /// Employee password.
        /// </param>
        /// <returns>
        /// T1 - true, if login went cerrectly, otherwise false.
        /// T2 - message.
        /// </returns>
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            Employee? employee = this.employeeService.GetEmployeeAndCheckCredentials(email, password);

            if (employee != null)
            {
                var credentials = $"{email}:{password}";
                var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                var basicAuthHeader = $"Basic {encodedCredentials}";

                return this.Ok(new { Success = true, Message = basicAuthHeader });
            }

            return this.Ok(new { Success = false, Message = "Invalid email or password" });
        }

        /// <summary>
        /// Post: /AnonymousUser/CreateAccount?email=[email]&password=[password].
        /// </summary>
        /// <param name="email">
        /// Employee email.
        /// </param>
        /// <param name="password">
        /// Employee password.
        /// </param>
        /// <returns>
        /// T1 - true, if login went cerrectly, otherwise false.
        /// T2 - message.
        /// </returns>
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(string email, string password)
        {
            if (!this.employeeService.AddNewEmployee(email, password))
            {
                return this.Ok(new { Success = false, Message = "There is already user with this email" });
            }

            return this.Ok(new { Success = true, Message = "Account created successfully" });
        }
    }
}
