namespace SoftwareMind_Intern_Challenge_Api_Tests
{
    using System.Collections;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Anonymous tests source class.
    /// </summary>
    public static class AnonymousTestsSource
    {
        public static IList<Employee> employeeList = new List<Employee>()
        {
            new Employee { Email = "newuser@example.com", Password = "newuserpassword" },
            new Employee { Email = "anotherone@example.com", Password = "andanother" },
            new Employee { Email = "wROngBOT", Password = "thisisntpasswordyouarelookingfor" },
        };

        /// <summary>
        /// Gets create account test scenarios.
        /// </summary>
        public static IEnumerable CreateAccountTestsSource
        {
            get
            {
                yield return new TestCaseData(employeeList[0], "True", "Account created successfully")
                    .SetDescription("Test try create account for first time")
                    .SetName("CreateAccountTestsSource");
                yield return new TestCaseData(employeeList[0], "False", "There is already user with this email")
                    .SetDescription("Test try create account for second time")
                    .SetName("CreateAccountTestsSource");
                yield return new TestCaseData(employeeList[1], "True", "Account created successfully")
                    .SetDescription("Test try create account for first time")
                    .SetName("CreateAccountTestsSource");
            }
        }

        /// <summary>
        /// Gets logic account test scenarios.
        /// </summary>
        public static IEnumerable LoginAccountTestsSource
        {
            get
            {
                yield return new TestCaseData(employeeList[0], "True", "Basic: ")
                    .SetDescription("Test try login with correct credentials")
                    .SetName("LoginAccountTestsSource");
                yield return new TestCaseData(employeeList[1], "True",  "Basic: ")
                    .SetDescription("Test try login with correct credentials")
                    .SetName("LoginAccountTestsSource");
                yield return new TestCaseData(employeeList[2], "False", "Invalid email or password")
                    .SetDescription("Test try login with wrong credentials")
                    .SetName("LoginAccountTestsSource");
            }
        }
    }
}