using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SoftwareMind_Intern_Challenge.Services;
using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SoftwareMind_Intern_Challenge.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly EmployeeService employeeService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            EmployeeService employeeService) : base(options, logger, encoder, clock)
        {
            this.employeeService = employeeService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = authorizationHeader.Substring(6);
            var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var credentials = credentialAsString.Split(":");
            if (credentials?.Length != 2)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }


            Employee employee = await this.employeeService.GetEmployeeAndCheckCredentials(credentials[0], credentials[1]);

            if (employee == null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
            else
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role)
                };
                var identity = new ClaimsIdentity(claims, "basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
            }
        }
    }
}
