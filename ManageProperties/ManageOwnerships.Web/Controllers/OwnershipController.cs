using ManageOwnerships.Domain.Models;
using ManageOwnerships.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ManageOwnerships.Web.Controllers
{
    /// <summary>
    /// Ownership Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnershipController : ControllerBase
    {
        private readonly IOwnershipsService _ownershipsService;

        public OwnershipController(IOwnershipsService ownershipsService)
        {
            _ownershipsService = ownershipsService;
        }

        /// <summary>
        /// Get all ownerships from owner id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllOwnerships")]
        public JsonResult GetAllOwnerships()
        {
            int ownerId = 0; // Get from token
            return new JsonResult(_ownershipsService.GetAllOwnerships(ownerId));
        }

        /// <summary>
        /// Get ownership from id
        /// </summary>
        /// <param name="ownershipId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOwnership/{ownershipId}")]
        public JsonResult GetOwnership(int ownershipId)
        {
            int ownerId = 0; // Get from token
            return new JsonResult(_ownershipsService.GetOwnership(ownershipId, ownerId));
        }

        /// <summary>
        /// Update ownership
        /// </summary>
        /// <param name="ownershipModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOwnership")]
        public JsonResult UpdateOwnership([FromBody] OwnershipModel ownershipModel)
        {
            ownershipModel.OwnerId = 0; // Get ownerId from token
            return new JsonResult(_ownershipsService.UpdateOwnership(ownershipModel));
        }

        /// <summary>
        /// Delete ownership from id
        /// </summary>
        /// <param name="ownershipId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteOwnership/{ownershipId}")]
        public JsonResult DeleteOwnership(int ownershipId)
        {
            int ownerId = 0; // Get ownerId from token
            return new JsonResult(_ownershipsService.DeleteOwnership(ownershipId, ownerId));
        }

        /// <summary>
        /// Add ownership
        /// </summary>
        /// <param name="ownershipModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOwnership")]
        public async Task<JsonResult> AddOwnership([FromBody] OwnershipModel ownershipModel)
        {
            ownershipModel.OwnerId = 0; // Get ownerId from token
            return new JsonResult(await _ownershipsService.AddOwnership(ownershipModel));
        }
    }
}
