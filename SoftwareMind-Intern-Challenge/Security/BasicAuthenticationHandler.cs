namespace SoftwareMind_Intern_Challenge.Security
{
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Options;
    using SoftwareMind_Intern_Challenge.Services;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Basic authentication handler.
    /// </summary>
    /// <param name="options">
    /// Options used by basic authentication handler.
    /// </param>
    /// <param name="logger">
    /// Logging system.
    /// </param>
    /// <param name="encoder">
    /// Url character encoder.
    /// </param>
    /// <param name="clock">
    /// System clock.
    /// </param>
    /// <param name="employeeService">
    /// Employee service.
    /// </param>
    public class BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        EmployeeService employeeService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
    {
        /// <summary>
        /// Employee service.
        /// </summary>
        private readonly EmployeeService employeeService = employeeService;

        /// <inheritdoc/>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!this.Request.Headers.ContainsKey("Authorization"))
            {
                var identity = new ClaimsIdentity();
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name));
            }

            string? authorizationHeader = this.Request.Headers["Authorization"];
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

            Employee? employee = this.employeeService.GetEmployeeAndCheckCredentials(credentials[0], credentials[1]);

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
                    new Claim(ClaimTypes.Role, employee.Role),
                };
                var identity = new ClaimsIdentity(claims, "basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name));
            }
        }
    }
}
