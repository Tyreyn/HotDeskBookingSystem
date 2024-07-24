namespace SoftwareMind_Intern_Challenge_Api_Tests.TestsCaseSource
{
    using System.Collections;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Admin tests source class.
    /// </summary>
    public static class AdminTestsSource
    {
        /// <summary>
        /// Admin class.
        /// </summary>
        public static readonly Employee Admin = new Employee
        {
            Id = 1,
            Role = "admin",
            Email = "admin@admin.com",
            Password = "Admin*123",
        };

        /// <summary>
        /// Gets desk operations test scenarios.
        /// </summary>
        public static IEnumerable DeskOperationsScenarioSource
        {
            get
            {
                yield return new TestCaseData("False", "There is no desks with this ID", 125434333)
                    .SetDescription("Test is trying delete desk that doesn't exists.")
                    .SetName("DeskOperationsScenarioSource");
                yield return new TestCaseData("False", "There is still reservations for this desk", 1)
                    .SetDescription("Test is trying delete desk on which is reservation.")
                    .SetName("DeskOperationsScenarioSource");
                yield return new TestCaseData("True", "Desk 1 deleted successfully", 1)
                    .SetDescription("Test remove desk with proper way")
                    .SetName("DeskOperationsScenarioSource");
            }
        }

        /// <summary>
        /// Gets location operations test scenarios.
        /// </summary>
        public static IEnumerable LocationOperationsScenarioSource
        {
            get
            {
                yield return new TestCaseData("False", "You can't remove 'unused desks' location!", 1)
                    .SetDescription("Test is trying remove 'unused desks' location")
                    .SetName("LocationOperationsScenarioSource");
                yield return new TestCaseData("False", "There is no location with this ID", 125434333)
                    .SetDescription("Test is trying delete location that doesn't exists.").
                    SetName("LocationOperationsScenarioSource");
                yield return new TestCaseData("False", "There are still some desk in this location!", 2)
                    .SetDescription("Test is trying to remove location with desks inside.")
                    .SetName("LocationOperationsScenarioSource");
                yield return new TestCaseData("True", "Location 2 deleted successfully", 2)
                    .SetDescription("Test remove location with proper way")
                    .SetName("LocationOperationsScenarioSource");
            }
        }
    }
}
