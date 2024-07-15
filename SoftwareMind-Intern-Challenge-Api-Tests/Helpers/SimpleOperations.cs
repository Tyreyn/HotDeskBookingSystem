namespace SoftwareMind_Intern_Challenge_Api_Tests.Helpers
{
    using System.Text;
    using System.Text.Json.Serialization;
    using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Serilog;
    using Serilog.Events;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Simple operations class.
    /// </summary>
    public class SimpleOperations(HttpClient testClient)
    {
        /// <summary>
        /// Http client class.
        /// </summary>
        private readonly HttpClient testClient = testClient;

        /// <summary>
        /// Http post request.
        /// </summary>
        /// <param name="url">
        /// URL request string.
        /// </param>
        /// <returns>
        /// HttpResponseMessage from response.
        /// </returns>
        public HttpResponseMessage PostRequest(string url)
        {
            StringContent content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            Log.Logger.Information($"Post request to {$"{this.testClient.BaseAddress}/{url}"} url");
            HttpResponseMessage response = this.testClient.PostAsync(url, content).Result;
            Log.Logger.Information($"Status code from response {response?.StatusCode}");
            string responseString = response!.Content.ReadAsStringAsync().Result;
            Log.Logger.Information($"Response: {responseString}");

            return response;
        }

        /// <summary>
        /// Http get request.
        /// </summary>
        /// <param name="url">
        /// URL request string.
        /// </param>
        /// <returns>
        /// HttpResponseMessage from response.
        /// </returns>
        public HttpResponseMessage GetRequest(string url)
        {
            Log.Logger.Information($"Post request to {$"{this.testClient.BaseAddress}/{url}"} url");
            HttpResponseMessage response = this.testClient.GetAsync(url).Result;
            Log.Logger.Information($"Status code from response {response?.StatusCode}");
            string responseString = response!.Content.ReadAsStringAsync().Result;
            Log.Logger.Information($"Response: {responseString}");

            return response;
        }

        /// <summary>
        /// Get all desks.
        /// </summary>
        /// <param name="success">
        /// Expected success string.
        /// </param>
        /// <param name="message">
        /// Expected message string.
        /// </param>
        /// <returns>
        /// List of desks.
        /// </returns>
        public IList<Desk> GetAllDesks(string success = "True", string message = "")
        {
            string url = $"/Employee/GetAvailableDesks";
            HttpResponseMessage response = this.GetRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            if ((int)response.StatusCode == 200)
            {
                return JsonConvert.DeserializeObject<IList<Desk>>(responseString);
            }
            else
            {
                return null;
            }
        }

        public void AddNewUser(string email, string password, string success = "True", string message = "")
        {
            string url = $"AnonymousUser/CreateAccount?email={email}&password={password}";
            HttpResponseMessage response = this.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That((int)response.StatusCode, Is.EqualTo(200), assertMessage);

            this.CheckIfResponseIsAsExpected(responseString, success, message);
        }

        /// <summary>
        /// Change desk in reservation.
        /// </summary>
        /// <param name="reservationId">
        /// Reservation from which desk will be changed.
        /// </param>
        /// <param name="deskId">
        /// New desk id for reservation.
        /// </param>
        /// <param name="success">
        /// Expected success string.
        /// </param>
        /// <param name="message">
        /// Expected message string.
        /// </param>
        public void ChangeReservationDesk(int reservationId, int deskId, string success = "True", string message = "")
        {
            string url = $"/Employee/ChangeReservationDesk?reservationId={reservationId}&newDeskId={deskId}";

            HttpResponseMessage response = this.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That((int)response.StatusCode, Is.EqualTo(200), assertMessage);

            this.CheckIfResponseIsAsExpected(responseString, success, message);
        }

        /// <summary>
        /// Delete desk.
        /// </summary>
        /// <param name="deskId">
        /// Desk id to be deleted.
        /// </param>
        /// <param name="success">
        /// Expected success string.
        /// </param>
        /// <param name="message">
        /// Expected message string.
        /// </param>
        public void DeleteDesk(int deskId, string success = "True", string message = "")
        {
            string url = $"/Admin/DeleteDesk?deskId={deskId}";

            HttpResponseMessage response = this.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That((int)response.StatusCode, Is.EqualTo(200), assertMessage);

            this.CheckIfResponseIsAsExpected(responseString, success, message);
        }

        /// <summary>
        /// Add new desk.
        /// </summary>
        /// <param name="locationId">
        /// Location id for which desk will be added.
        /// </param>
        /// <param name="responseCode">
        /// Expected response code.
        /// </param>
        public void AddDesk(string locationId = "1", int responseCode = 200)
        {
            Log.Logger.Information($"add desk to location: {1}");
            string url = $"/Admin/AddNewDesk?locationId={locationId}";

            HttpResponseMessage response = this.PostRequest(url);

            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: {responseCode}";
            Assert.That((int)response.StatusCode, Is.EqualTo(responseCode), assertMessage);
        }

        /// <summary>
        /// Add new location.
        /// </summary>
        /// <param name="name">
        /// New location name.
        /// </param>
        /// <param name="responseCode">
        /// Expected response code.
        /// </param>
        public void AddLocation(string name = "Test Room", int responseCode = 200)
        {
            Log.Logger.Information($"add new location: {name}");
            string url = $"/Admin/AddNewLocation?locationName={name}";

            HttpResponseMessage response = this.PostRequest(url);

            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: {responseCode}";
            Assert.That((int)response.StatusCode, Is.EqualTo(responseCode), assertMessage);

        }

        /// <summary>
        /// Make reservation.
        /// </summary>
        /// <param name="deskId">
        /// Desk to be booked.
        /// </param>
        /// <param name="success">
        /// Expected success string.
        /// </param>
        /// <param name="message">
        /// Expected message string.
        /// </param>
        public void MakeReservation(int deskId, string success = "True", string message = "")
        {
            string url = $"/Employee/MakeReservation?deskId={deskId}" +
                $"&dateStart={DateTime.Now.Date.AddDays(5).ToString("MM/dd/yyyy")}" +
                $"&dateEnd={DateTime.Now.Date.AddDays(10).ToString("MM/dd/yyyy")}";

            HttpResponseMessage response = this.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            string assertMessage = $"Status code is not as expected.\n " +
                $"Actual: {(int)response.StatusCode}, Expected: 200";
            Assert.That((int)response.StatusCode, Is.EqualTo(200), assertMessage);

            this.CheckIfResponseIsAsExpected(responseString, success, message);
        }

        /// <summary>
        /// Login to account.
        /// </summary>
        /// <param name="email">
        /// User email.
        /// </param>
        /// <param name="password">
        /// User password.
        /// </param>
        /// <param name="success">
        /// Expected success string.
        /// </param>
        /// <param name="message">
        /// Expected message string.
        /// </param>
        /// <returns>
        /// Authentication string.
        /// </returns>
        public string LoginToAccount(string email, string password, string success = "True", string message = "")
        {
            string url = $"AnonymousUser/Login?email={email}&password={password}";

            HttpResponseMessage response = this.PostRequest(url);
            string responseString = response.Content.ReadAsStringAsync().Result;

            if (JObject.Parse(responseString)["success"]!.ToString() == success)
            {
                string credentials = $"{email}:{password}";
                string encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                message = $"Basic {encodedCredentials}";
            }

            return this.CheckIfResponseIsAsExpected(responseString, success, message) ? message.Split(' ')[1] : string.Empty;
        }

        /// <summary>
        /// Check if response is as expected.
        /// </summary>
        /// <param name="responseString">
        /// Response string.
        /// </param>
        /// <param name="success">
        /// Expected success string.
        /// </param>
        /// <param name="message">
        /// Expected message string.
        /// </param>
        /// True, if response is as expected, otherwise false.
        /// </returns>
        private bool CheckIfResponseIsAsExpected(string responseString, string success, string message)
        {
            bool result = true;
            string assertMessage = "Response success value is {0}as expected.\n Actual: {1}, Expected: {2}";

            if (JObject.Parse(responseString)["success"]!.ToString() != success)
            {
                result &= false;
            }

            Log.Logger.Write(
                result ? LogEventLevel.Information : LogEventLevel.Warning,
                string.Format(
                    assertMessage,
                    result ? string.Empty : "not ",
                    JObject.Parse(responseString)["success"],
                    success));

            if (message != string.Empty)
            {
                assertMessage = "Response message value is {0}as expected.\n Actual: {1}, Expected: {1}";
                if (JObject.Parse(responseString)["message"]!.ToString() != message)
                {
                    result &= false;
                }
            }

            Log.Logger.Write(
                result ? LogEventLevel.Information : LogEventLevel.Warning,
                string.Format(
                    assertMessage,
                    result ? string.Empty : "not ",
                    JObject.Parse(responseString)["success"],
                    success));

            return result;
        }
    }
}
