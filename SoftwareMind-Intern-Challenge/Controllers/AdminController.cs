using Microsoft.AspNetCore.Mvc;
using SoftwareMind_Intern_Challenge.Services;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace SoftwareMind_Intern_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController(ILogger<EmployeeController> logger, DeskService deskService, LocationService locationService) : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger = logger;

        private readonly DeskService deskService = deskService;

        private readonly LocationService locationService = locationService;

        private readonly JsonSerializerSettings options = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        [HttpPost("AddNewDesk")]
        public IActionResult AddNewDesk(int LocationId = 1)
        {
            Desk newDesk = new Desk();
            newDesk.LocationId = LocationId;
            this.deskService.AddDesk(newDesk);
            return this.Ok();
        }

        [HttpPost("ChangeDeskStatus")]
        public IActionResult ChangeDeskStatus(int locationId)
        {

            this.deskService.ChangeDeskAvailable(locationId);
            return this.Ok();
        }

        [HttpPost("ChangeDeskLocation")]
        public IActionResult ChangeDeskLocation(int locationId, int deskId)
        {
            (bool,string) response = this.deskService.ChangeDeskLocation(locationId, deskId);
            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }
            return this.Ok(new { Success = false, Message = response.Item2 });
        }

        [HttpPost("GetAllDesks")]
        public IActionResult GetAllDesks()
        {
            IList<Desk> desk = this.deskService.GetDesks();
            string deskJson = JsonConvert.SerializeObject(desk, options);
            return this.Ok(deskJson);
        }

        [HttpPost("GetAllLocations")]
        public IActionResult GetAllLocations()
        {
            IList<Location> locations = this.locationService.GetLocations();
            string locationsJson = JsonConvert.SerializeObject(locations, options);
            return this.Ok(locationsJson);
        }


        [HttpPost("DeleteDesk")]
        public IActionResult DeleteDesk(int deskId)
        {
            (bool, string) response = this.deskService.DeleteDesk(deskId);
            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }
            return this.Ok(new { Success = false, Message = response.Item2 });
        }

        [HttpPost("AddNewLocation")]
        public IActionResult AddNewLocation(string locationName)
        {
            Location location = new Location { Name = locationName };
            this.locationService.AddNewLocation(location);
            return this.Ok();
        }

        [HttpPost("DeleteLocation")]
        public IActionResult DeleteLocation(int locationId)
        {
            (bool,string) response = this.locationService.DeleteLocation(locationId);
            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }
            return this.Ok(new { Success = false, Message = response.Item2 });
        }

        [HttpPost("ChangeLocationName")]
        public IActionResult ChangeLocationName(int locationId,  string newName)
        {
            (bool, string) response = this.locationService.ChangeLocationName(locationId, newName);
            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }
            return this.Ok(new { Success = false, Message = response.Item2 });
        }
    }
}
