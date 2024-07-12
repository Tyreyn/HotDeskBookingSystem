using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using SoftwareMind_Intern_Challenge.Services;
using SoftwareMind_Intern_ChallengeDTO.Data;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using System.Security.Claims;
using System.Text;

namespace SoftwareMind_Intern_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AnonymousUserController : ControllerBase
    {
        private readonly ILogger<AnonymousUserController> _logger;

        private readonly EmployeeService employeeService;

        public AnonymousUserController(ILogger<AnonymousUserController> logger, EmployeeService employeeService)
        {
            _logger = logger;
            this.employeeService = employeeService;
        }

        /// <summary>
        /// POST: /AnonymousUserController/Login/email/password
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            Employee? employee = this.employeeService.GetEmployeeAndCheckCredentials(email, password).Result;

            if (employee != null)
            {
                var credentials = $"{email}:{password}";
                var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                var basicAuthHeader = $"Basic {encodedCredentials}";

                return this.Ok(new { Success = true, Authorization = basicAuthHeader });
            }

            return this.Unauthorized(new { Success = false, ErrorMessage = "Invalid email or password" });
        }

        [HttpGet("CreateAccount")]
        public IActionResult CreateAccount(string email, string password)
        {
            if (!this.employeeService.AddNewEmployee(email, password).Result)
            {
                return this.Problem("There is already user with this email");
            }

            return this.Ok();
        }
    }
}
