using ManageOwnerships.Domain.Models;
using ManageOwnerships.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageOwnerships.Web.Controllers
{
    /// <summary>
    /// Owner controller crud
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Add owner
        /// </summary>
        /// <param name="ownerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOwner")]
        public JsonResult AddOwner([FromBody] OwnerModel ownerModel)
        {
            return new JsonResult(_ownerService.AddOwner(ownerModel));
        }

        /// <summary>
        /// Get owner from owner id
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOwner/{ownerId}")]
        public JsonResult GetOwner(int ownerId)
        {
            return new JsonResult(_ownerService.GetOwner(ownerId));
        }

        /// <summary>
        /// Update owner Ownerships
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateOwner")]
        public JsonResult UpdateOwner([FromBody] OwnerModel ownerModel)
        {
            return new JsonResult(_ownerService.UpdateOwner(ownerModel));
        }

        /// <summary>
        /// Delete owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteOwner/{ownerId}")]
        public JsonResult Delete(int ownerId)
        {
            return new JsonResult(_ownerService.DeleteOwner(ownerId));
        }
    }
}
