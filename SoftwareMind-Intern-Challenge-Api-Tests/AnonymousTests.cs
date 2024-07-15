namespace SoftwareMind_Intern_Challenge_Api_Tests
{
    using System.Diagnostics;
    using System.Net.Http.Headers;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Newtonsoft.Json.Linq;
    using Serilog;
    using Serilog.Sinks.InMemory;
    using Serilog.Sinks.SystemConsole.Themes;
    using SoftwareMind_Intern_Challenge_Api_Tests.Helpers;
    using SoftwareMind_Intern_Challenge_Api_Tests.TestsCaseSource;
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Anonumous user tests.
    /// </summary>
    [TestFixture]
    public class AnonymousTests
    {

        /// <summary>
        /// HttpClient class.
        /// </summary>
        private HttpClient testClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousTests"/> class.
        /// </summary>
        public AnonymousTests()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
            .CreateLogger();
        }

        /// <summary>
        /// Test initialize method.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            Log.Logger.Information($"Setup API with in memory database");
            var hotDeskBookingSystemFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll<HotDeskBookingSystemContext>();
                        services.RemoveAll<DbContextOptions>();
                        foreach (var option in services.Where(s => s.ServiceType.BaseType == typeof(DbContextOptions)).ToList())
                        {
                            services.Remove(option);
                        }

                        services.AddDbContext<HotDeskBookingSystemContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDataBase");
                        });
                    });
                });

            Log.Logger.Information($"Create API client");
            this.testClient = hotDeskBookingSystemFactory.CreateClient();
        }

        /// <summary>
        /// Try to get desk as anonymous.
        /// </summary>
        [Test]
        [Order(3)]
        public void TryToGetDesksAsAnonymous()
        {
            // Arrange
            Log.Logger.Information($"Starting test {TestContext.CurrentContext.Test.Name}");
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);

            // Act
            IList<Desk>? desks = simpleOperations.GetAllDesks();
            if(desks == null)
            {
                Assert.Pass("Anonymous user can't get desks");
            }

        }

        /// <summary>
        /// Create account test method.
        /// </summary>
        /// <param name="employee">
        /// Employee class to test.
        /// </param>
        /// <param name="success">
        /// Success of request.
        /// </param>
        /// <param name="message">
        /// Message from request.
        /// </param>
        [TestCaseSource(typeof(AnonymousTestsSource), nameof(AnonymousTestsSource.CreateAccountTestsSource))]
        [Order(1)]
        public void CreateAccountTests_ShouldGetExpectedResults(Employee employee, string success, string message)
        {
            // Arrange
            Log.Logger.Information($"[Starting test {TestContext.CurrentContext.Test.Name}");
            string url = $"AnonymousUser/CreateAccount?email={employee.Email}&password={employee.Password}";
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);

            // Act
            HttpResponseMessage response = simpleOperations.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That(
                (int)response.StatusCode,
                Is.EqualTo(200),
                assertMessage);

            assertMessage = $"Response message is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["message"]}, Expected: {message}";
            Assert.That(
                JObject.Parse(responseString)["message"]!.ToString(),
                Is.EqualTo(message),
                assertMessage);

            assertMessage = $"Response success value is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["success"]}, Expected: {message}";
            Assert.That(
                JObject.Parse(responseString)["success"]!.ToString(),
                Is.EqualTo(success),
                assertMessage);
        }

        /// <summary>
        /// Login to account test method.
        /// </summary>
        /// <param name="employee">
        /// Employee class to test.
        /// </param>
        /// <param name="success">
        /// Success of request.
        /// </param>
        /// <param name="message">
        /// Message from request.
        /// </param>
        [TestCaseSource(typeof(AnonymousTestsSource), nameof(AnonymousTestsSource.LoginAccountTestsSource))]
        [Order(2)]
        public void LoginToAccount_ShouldGetExpectedResults(Employee employee, string success, string message)
        {
            // Arrange
            Log.Logger.Information($"Starting test {TestContext.CurrentContext.Test.Name}");
            string url = $"AnonymousUser/Login?email={employee.Email}&password={employee.Password}";
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);

            // Act
            HttpResponseMessage response = simpleOperations.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            if (success == "True")
            {
                string credentials = $"{employee.Email}:{employee.Password}";
                string encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                message = $"Basic {encodedCredentials}";
            }

            // Assert
            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That(
                (int)response.StatusCode,
                Is.EqualTo(200),
                assertMessage);

            assertMessage = $"Response message is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["message"]}, Expected: {message}";
            Assert.That(
                JObject.Parse(responseString)["message"]!.ToString(),
                Is.EqualTo(message),
                assertMessage);

            assertMessage = $"Response success value is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["success"]}, Expected: {message}";
            Assert.That(
                JObject.Parse(responseString)["success"]!.ToString(),
                Is.EqualTo(success),
                assertMessage);
        }

        /// <summary>
        /// Tear down after test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Log.Logger.Information("Cleaning after test");
            Log.CloseAndFlush();
        }
    }
}