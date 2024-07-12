using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftwareMind_Intern_Challenge.Services;
using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using System.ComponentModel;
using System.Security.Claims;
using System.Text.Json;

namespace SoftwareMind_Intern_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "user, admin")]
    public class EmployeeController(ILogger<EmployeeController> logger, ReservationService reservationService, DeskService deskService) : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger = logger;

        private readonly ReservationService reservationService = reservationService;

        private readonly DeskService deskService = deskService;

        private readonly JsonSerializerSettings options = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };


        [HttpGet("GetAvailableDesks")]
        public IActionResult GetAvailableDesks()
        {
            IList<Desk> desks = this.deskService.GetDesks();
            var role = this.User?.FindFirst(ClaimTypes.Role);
            if (role.Value != "admin")
                desks.Where(d => d.Reservations != null).ToList().ForEach(d => d.Reservations = null);
            string deskJson = JsonConvert.SerializeObject(desks, options);
            return this.Ok(deskJson);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="deskId"></param>
        /// <param name="dataStart">Mm-Dd-Yyyy</param>
        /// <param name="dataEnd">Mm-Dd-Yyyy</param>
        /// <returns></returns>
        [HttpPost("MakeReservation")]
        [Description("DateTime is US formated MM-DD-YYYY")]
        public IActionResult MakeReservation(
            int deskId,
            [Description("DateTime is US formated MM-DD-YYYY")] DateTime dataStart,
            [Description("DateTime is US formated MM-DD-YYYY")] DateTime dataEnd)
        {
            var userName = this.User?.FindFirst(ClaimTypes.NameIdentifier);
            (bool, string) response = this.reservationService.MakeReservation(new Reservation
            {
                EmployeeId = Int32.Parse(userName.Value),
                DeskId = deskId,
                DataStart = dataStart,
                DataEnd = dataEnd
            });


            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }
            return this.Ok(new { Success = false, Message = response.Item2 });
        }
    }
}
