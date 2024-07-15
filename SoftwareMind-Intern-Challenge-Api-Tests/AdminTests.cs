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
    /// Admin user tests.
    /// </summary>
    [TestFixture]
    public class AdminTests
    {
        /// <summary>
        /// HttpClient class.
        /// </summary>
        private HttpClient testClient;

        /// <summary>
        /// Test database context.
        /// </summary>
        private DbContext? dbContext;

        /// <summary>
        /// Test initialize method.
        /// </summary>
        [SetUp]
        public void Init()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
            .CreateLogger();

            Log.Logger.Fatal("Delete database after test.");
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
                            options.UseInMemoryDatabase("TestDb");
                        });

                        var serviceProvider = services.BuildServiceProvider();

                        var scope = serviceProvider.CreateScope();
                        this.dbContext = scope.ServiceProvider.GetRequiredService<HotDeskBookingSystemContext>();

                        this.dbContext.Database.EnsureCreated();
                    });
                });

            Log.Logger.Information($"Create API client");
            this.testClient = hotDeskBookingSystemFactory.CreateClient();
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);
            this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                simpleOperations.LoginToAccount(
                    AdminTestsSource.Admin.Email!,
                    AdminTestsSource.Admin.Password!));
        }

        /// <summary>
        /// Try to get desk as admin.
        /// </summary>
        [Test]
        [Order(3)]
        public void TryToGetDesksAsAdmin()
        {
            // Arrange
            int desksNumber = 10;
            Log.Logger.Information($"Starting test {TestContext.CurrentContext.Test.Name}");
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);

            // Act
            for (int i = 0; i < desksNumber; i++)
            {
                simpleOperations.AddDesk();
            }

            IList<Desk>? desks = simpleOperations.GetAllDesks();

            // Assert
            Assert.That(desks.Count() == desksNumber, "Number of added desks is as expected.");
        }

            /// <summary>
            /// Desk operations method.
            /// </summary>
            /// <param name="success">
            /// Success of request.
            /// </param>
            /// <param name="message">
            /// Message from request.
            /// </param>
            /// <param name="deskId">
            /// Id of tested desk.
            /// </param>
            [TestCaseSource(typeof(AdminTestsSource), nameof(AdminTestsSource.DeskOperationsScenarioSource))]
        public void DeskOperationsScenario(string success, string message, int deskId)
        {
            // Arrange
            Log.Logger.Information(new string('*', 20));
            Log.Logger.Information($"{TestContext.CurrentContext.Test.Name} - " +
                $"{TestContext.CurrentContext.Test.Properties.Get("Description")}");
            string url = $"/Admin/DeleteDesk?deskId={deskId}";
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);

            // Act
            simpleOperations.AddLocation(); // locationId: 2
            simpleOperations.AddDesk("2"); // deskId: 1
            simpleOperations.MakeReservation(1, success, message);

            if (success == "True")
            {
                simpleOperations.AddDesk("2"); // deskId: 2
                simpleOperations.ChangeReservationDesk(1, 2);
            }

            HttpResponseMessage response = simpleOperations.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That((int)response.StatusCode, Is.EqualTo(200), assertMessage);

            assertMessage = $"Response message is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["message"]}, Expected: {message}";

            Assert.That(
                JObject.Parse(responseString)["message"]!.ToString(),
                Is.EqualTo(message),
                assertMessage);

            assertMessage = $"Response success value is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["success"]}, Expected: {success}";
            Assert.That(
                JObject.Parse(responseString)["success"]!.ToString(),
                Is.EqualTo(success),
                assertMessage);
        }

        /// <summary>
        /// Location operations method.
        /// </summary>
        /// <param name="success">
        /// Success of request.
        /// </param>
        /// <param name="message">
        /// Message from request.
        /// </param>
        /// <param name="locationId">
        /// Id of tested location.
        /// </param>
        [TestCaseSource(typeof(AdminTestsSource), nameof(AdminTestsSource.LocationOperationsScenarioSource))]
        public void LocationOperationsScenario(string success, string message, int locationId)
        {
            // Arrange
            string url = $"/Admin/DeleteLocation?locationId={locationId}";
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);

            // Act
            simpleOperations.AddLocation();
            simpleOperations.AddDesk("2");

            if (success == "True")
            {
                simpleOperations.DeleteDesk(1);
            }

            HttpResponseMessage response = simpleOperations.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            // Assert
            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That((int)response.StatusCode, Is.EqualTo(200), assertMessage);

            assertMessage = $"Response message is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["message"]}, Expected: {message}";

            Assert.That(JObject.Parse(responseString)["message"] !.ToString(), Is.EqualTo(message), assertMessage);

            assertMessage = $"Response success value is not as expected.\n " +
                $"Actual: {JObject.Parse(responseString)["success"]}, Expected: {message}";
            Assert.That(JObject.Parse(responseString)["success"] !.ToString(), Is.EqualTo(success), assertMessage);
        }

        /// <summary>
        /// Tear down after test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Log.Logger.Information("Cleaning after test");
            if (this.dbContext!.Database.IsInMemory())
            {
                Log.Logger.Error("Delete database after test.");
                this.dbContext.Database.EnsureDeleted();
            }

            Log.Logger.Information("\n\n" + new string('*', 100));
            this.testClient.Dispose();
            Log.CloseAndFlush();
        }
    }
}
