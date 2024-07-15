namespace SoftwareMind_Intern_Challenge.Controllers
{
    using System.Data;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SoftwareMind_Intern_Challenge.Services;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Employee controller.
    /// </summary>
    /// <param name="reservationService">
    /// Reservation service.
    /// </param>
    /// <param name="deskService">
    /// Desk service.
    /// </param>
    /// <param name="locationService">
    /// Location service.
    /// </param>
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "user, admin")]
    public class EmployeeController(
        ReservationService reservationService,
        DeskService deskService,
        LocationService locationService) : ControllerBase
    {
        /// <summary>
        /// Reservation service.
        /// </summary>
        private readonly ReservationService reservationService = reservationService;

        /// <summary>
        /// Location service.
        /// </summary>
        private readonly LocationService locationService = locationService;

        /// <summary>
        /// Desk service.
        /// </summary>
        private readonly DeskService deskService = deskService;

        /// <summary>
        /// Json serializer options.
        /// </summary>
        private readonly JsonSerializerSettings options = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// Get: /Employee/GetAvailableDesks.
        /// </summary>
        /// <returns>
        /// OkObjectResult -> string JSON of list desk object.
        /// </returns>
        [HttpGet("GetAvailableDesks")]
        public IActionResult GetAvailableDesks()
        {
            string? userId = this.User?.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            this.reservationService.CheckIfReservationChanged();
            IList<Desk>? desks = this.deskService.GetDesks();
            string? role = this.User?.FindFirst(ClaimTypes.Role).Value.ToString();

            if (role != "admin")
            {
                IList<Reservation>? userReservations = this.reservationService.GetReservationByUserId(int.Parse(userId));
                desks.Where(d => d.Reservations != null).ToList().ForEach(d => d.Reservations = null);
                if (userReservations.Count > 0)
                {
                    foreach (Reservation reservation in userReservations)
                    {
                        desks.Where(d => d.Id == reservation.DeskId)
                            .ToList()
                            .ForEach(d =>
                            {
                                if (d.Reservations == null)
                                {
                                    d.Reservations = new List<Reservation>() { reservation };
                                }
                                else
                                {
                                    d.Reservations.Add(reservation);
                                }
                            });
                    }
                }
            }

            string deskJson = JsonConvert.SerializeObject(desks, this.options);
            return this.Ok(deskJson);
        }

        /// <summary>
        /// /Employee/FilterDesksByLocation?filter=[filter].
        /// </summary>
        /// <param name="filter">
        /// Location name filter.
        /// </param>
        /// <returns>
        /// OkObjectResult -> string JSON of list desk object.
        /// <returns>
        [HttpGet("FilterDesksByLocation")]
        public IActionResult FilterDesksByLocation(string filter)
        {
            this.reservationService.CheckIfReservationChanged();
            IList<Desk>? desks = this.deskService.GetDesks();
            string? role = this.User?.FindFirst(ClaimTypes.Role).Value.ToString();

            if (role != "admin")
            {
                desks.Where(d => d.Reservations != null).ToList().ForEach(d => d.Reservations = null);
            }

            desks = desks.Where(d => d.Location.Name.ToLower().Contains(filter.ToLower())).ToList();
            string deskJson = JsonConvert.SerializeObject(desks, this.options);
            return this.Ok(deskJson);
        }

        /// <summary>
        /// /Employee/GetAllLocations.
        /// </summary>
        /// <returns>
        /// OkObjectResult - JSON string of location list.
        /// </returns>
        [HttpPost("GetAllLocations")]
        public IActionResult GetAllLocations()
        {
            this.reservationService.CheckIfReservationChanged();
            IList<Location>? locations = this.locationService.GetLocations();
            string locationsJson = JsonConvert.SerializeObject(locations, this.options);
            return this.Ok(locationsJson);
        }

        /// <summary>
        /// /Employee/MakeReservation?deskId=[deskId]&dataStart=[dateStart]&dataEnd=[dateEnd]
        /// Make reservation.
        /// </summary>
        /// <param name="deskId">
        /// Desk Id to be booked.
        /// </param>
        /// <param name="dateStart">
        /// Start date of reservation.
        /// </param>
        /// <param name="dateEnd">
        /// End date of reservation.
        /// </param>
        /// <returns>
        /// OkObjectResult:
        /// Success - true if reservation made correctly, otherwise false.
        /// Message.
        /// </returns>
        [HttpPost("MakeReservation")]
        public IActionResult MakeReservation(
            int deskId,
            DateTime dateStart,
            DateTime dateEnd)
        {
            string? userId = this.User?.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            string? userEmail = this.User?.FindFirst(ClaimTypes.Email).Value.ToString();
            (bool, string) response = this.reservationService.MakeReservation(
                new Reservation
                {
                    EmployeeId = int.Parse(userId),
                    DeskId = deskId,
                    DateStart = dateStart,
                    DateEnd = dateEnd,
                },
                userEmail.ToString());

            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }

            return this.Ok(new { Success = false, Message = response.Item2 });
        }

        /// <summary>
        /// /Employee/ChangeReservationDesk?reservationId=[reservationId]&newDeskId=[&newDeskId].
        /// </summary>
        /// <param name="reservationId">
        /// ID of reservation.
        /// </param>
        /// <param name="newDeskId">
        /// New desk ID for reservation.
        /// </param>
        /// <returns>
        /// OkObjectResult:
        /// Success - true if reservation made correctly, otherwise false.
        /// Message.
        /// </returns>
        [HttpPost("ChangeReservationDesk")]
        public IActionResult ChangeReservationDesk(int reservationId, int newDeskId)
        {
            string? userId = this.User?.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            (bool, string) response = this.reservationService.ChangeReservationDesk(reservationId, newDeskId, int.Parse(userId));

            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }

            return this.Ok(new { Success = false, Message = response.Item2 });
        }
    }
}
