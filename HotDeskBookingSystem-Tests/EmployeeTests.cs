namespace SoftwareMind_Intern_Challenge_Api_Tests
{
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Serilog;
    using Serilog.Sinks.SystemConsole.Themes;
    using SoftwareMind_Intern_Challenge_Api_Tests.Helpers;
    using SoftwareMind_Intern_Challenge_Api_Tests.TestsCaseSource;
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Employee user tests.
    /// </summary>
    [TestFixture]
    public class EmployeeTests
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

        }

        /// <summary>
        /// Check if employee can add desk and location.
        /// </summary>
        [Test]
        public void CheckIfUserCanAddDeskAndLocation()
        {
            // Arrange
            Employee testEmployee = AnonymousTestsSource.employeeList[0];
            Log.Logger.Information($"Starting test {TestContext.CurrentContext.Test.Name}");
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);
            simpleOperations.AddNewUser(testEmployee.Email, testEmployee.Password);
            this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                simpleOperations.LoginToAccount(
                    testEmployee.Email!,
                    testEmployee.Password!));

            // Act
            simpleOperations.AddLocation(responseCode: 403);
            simpleOperations.AddDesk(responseCode: 403);
        }

        /// <summary>
        /// Check if user can see reservation.
        /// </summary>
        /// <param name="isAdmin">
        /// Indicates whenever user is admin.
        /// </param>
        [TestCase(true)]
        [TestCase(false)]
        public void CheckIfUserCanSeeReservationsOthersReservation(bool isAdmin)
        {
            // Arrange
            Employee testEmployee = AnonymousTestsSource.employeeList[0];
            Log.Logger.Information($"Starting test {TestContext.CurrentContext.Test.Name}");
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);
            this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                simpleOperations.LoginToAccount(
                    AdminTestsSource.Admin.Email!,
                    AdminTestsSource.Admin.Password!));
            simpleOperations.AddLocation();
            simpleOperations.AddDesk("2");
            simpleOperations.MakeReservation(1);

            // Act
            if (!isAdmin)
            {
                simpleOperations.AddNewUser(testEmployee.Email, testEmployee.Password);
                this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    simpleOperations.LoginToAccount(
                        testEmployee.Email!,
                        testEmployee.Password!));
            }
            IList<Desk>? desks = simpleOperations.GetAllDesks();

            // Assert
            if (isAdmin)
            {
                Assert.That(desks[0].Reservations.Count, Is.EqualTo(1), "Admin can see desk reservations.");
            }
            else
            {
                Assert.That(desks[0].Reservations, Is.EqualTo(null), "User cant see desk reservations.");
            }
        }

        /// <summary>
        /// Check if user can see reservation.
        /// </summary>
        /// <param name="isAdmin">
        /// Indicates whenever user is admin.
        /// </param>
        [TestCase(true)]
        [TestCase(false)]
        public void CheckIfUserCanSeeOwnReservations(bool isAdmin)
        {
            // Arrange
            Employee testEmployee = AnonymousTestsSource.employeeList[0];
            Log.Logger.Information($"Starting test {TestContext.CurrentContext.Test.Name}");
            SimpleOperations simpleOperations = new SimpleOperations(this.testClient);
            this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                simpleOperations.LoginToAccount(
                    AdminTestsSource.Admin.Email!,
                    AdminTestsSource.Admin.Password!));
            simpleOperations.AddLocation(); // LocationId: 2
            simpleOperations.AddDesk("2"); // deskId: 1
            simpleOperations.AddLocation(); // LocationId: 3
            simpleOperations.AddDesk("3"); // deskId: 2
            simpleOperations.MakeReservation(1);
            if (!isAdmin)
            {
                simpleOperations.AddNewUser(testEmployee.Email, testEmployee.Password);
                this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    simpleOperations.LoginToAccount(
                        testEmployee.Email!,
                        testEmployee.Password!));
                simpleOperations.MakeReservation(2);
            }
            else
            {
                simpleOperations.MakeReservation(2);
            }

            // Act
            IList<Desk>? desks = simpleOperations.GetAllDesks();

            // Assert
            if (isAdmin)
            {
                Assert.That(desks[0].Reservations.Count, Is.EqualTo(1), "Admin can see desk reservations.");
            }
            else
            {
                Assert.That(desks[0].Reservations, Is.EqualTo(null), "User can't see other desk reservations.");
                Assert.That(desks[1].Reservations.Count, Is.EqualTo(1), "User can see own desk reservations.");
            }
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
