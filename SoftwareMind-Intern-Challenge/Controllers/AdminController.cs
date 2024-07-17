namespace SoftwareMind_Intern_Challenge.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftwareMind_Intern_Challenge.Services;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Admin controller.
    /// </summary>
    /// <param name="deskService">
    /// Desk service.
    /// </param>
    /// <param name="locationService">
    /// Location service.
    /// </param>
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController(DeskService deskService, LocationService locationService) : ControllerBase
    {
        /// <summary>
        /// Desk service.
        /// </summary>
        private readonly DeskService deskService = deskService;

        /// <summary>
        /// Location service.
        /// </summary>
        private readonly LocationService locationService = locationService;

        /// <summary>
        /// Post: /Admin/CheckIfAdmin.
        /// </summary>
        /// <returns>
        /// OkObjectResult.
        /// </returns>
        [HttpPost("CheckIfAdmin")]
        public IActionResult CheckIfAdmin()
        {
            return this.Ok();
        }

        /// <summary>
        /// Post: /Admin/AddNewDesk?locationId=[locationId].
        /// </summary>
        /// <param name="locationId">
        /// Location Id of new desk.
        /// </param>
        /// <returns>
        /// OkObjectResult.
        /// </returns>
        [HttpPost("AddNewDesk")]
        public IActionResult AddNewDesk(int locationId = 1)
        {
            this.deskService.AddDesk(locationId);
            return this.Ok();
        }

        /// <summary>
        /// Post: /Admin/ChangeDeskStatus?deskId=[deskId].
        /// </summary>
        /// <param name="deskId">
        /// ID of desk to be updated.
        /// </param>
        /// <returns>
        /// OkObjectResult.
        /// </returns>
        [HttpPost("ChangeDeskStatus")]
        public IActionResult ChangeDeskStatus(int deskId)
        {
            this.deskService.ChangeDeskAvailable(deskId);
            return this.Ok();
        }

        /// <summary>
        /// Post: /Admin/ChangeDeskLocation?locationId=[locationId]&deskId=[deskId].
        /// </summary>
        /// <param name="locationId">
        /// New location ID.
        /// </param>
        /// <param name="deskId">
        /// ID of desk that location will be changed.
        /// </param>
        /// <returns>
        /// T1 - true, if desk location changed correctly, otherwise false.
        /// T2 - message.
        /// </returns>
        [HttpPost("ChangeDeskLocation")]
        public IActionResult ChangeDeskLocation(int locationId, int deskId)
        {
            (bool, string) response = this.deskService.ChangeDeskLocation(locationId, deskId);
            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }

            return this.Ok(new { Success = false, Message = response.Item2 });
        }

        /// <summary>
        /// Post: /Admin/DeleteDesk?deskId=[deskId].
        /// </summary>
        /// <param name="deskId">
        /// Id of desk to be deleted.
        /// </param>
        /// <returns>
        /// T1 - true, if desk deleted correctly, otherwise false.
        /// T2 - message.
        /// </returns>
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

        /// <summary>
        /// Post: /Admin/AddNewLocation?locationName=[locationName].
        /// </summary>
        /// <param name="locationName">
        /// New location name.
        /// </param>
        /// <returns>
        /// OkObjectResult.
        /// </returns>
        [HttpPost("AddNewLocation")]
        public IActionResult AddNewLocation(string locationName)
        {
            Location location = new Location { Name = locationName };
            this.locationService.AddNewLocation(location);
            return this.Ok();
        }

        /// <summary>
        /// Post: /Admin/DeleteLocation?locationId=[locationId].
        /// </summary>
        /// <param name="locationId">
        /// ID of location to be deleted.
        /// </param>
        /// <returns>
        /// OkObjectResult.
        /// </returns>
        [HttpPost("DeleteLocation")]
        public IActionResult DeleteLocation(int locationId)
        {
            (bool, string) response = this.locationService.DeleteLocation(locationId);
            if (response.Item1)
            {
                return this.Ok(new { Success = true, Message = response.Item2 });
            }

            return this.Ok(new { Success = false, Message = response.Item2 });
        }

        /// <summary>
        /// Post: /Admin/ChangeLocationName?locationId=[locationId]&newName=[newName].
        /// </summary>
        /// <param name="locationId">
        /// ID of location to be updated.
        /// </param>
        /// <param name="newName">
        /// New name for location.
        /// </param>
        /// <returns>
        /// T1 - true, if desk deleted correctly, otherwise false.
        /// T2 - message.
        /// </returns>
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
