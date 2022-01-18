using ManageProperties.Domain.Models;
using ManageProperties.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManageProperties.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }

        [HttpGet]
        [Route("GetAllProperties")]
        public JsonResult GetAllProperties()
        {
            int ownerId = 0; // Get from token
            return new JsonResult(_propertiesService.GetAllProperties(ownerId));
        }

        [HttpGet]
        [Route("GetProperty/{}")]
        public JsonResult GetProperty(int propertyId)
        {
            int ownerId = 0; // Get from token
            return new JsonResult(_propertiesService.GetProperty(propertyId, ownerId));
        }

        [HttpPost]
        [Route("UpdateProperty")]
        public JsonResult UpdateProperty([FromBody] PropertyModel propertyModel)
        {
            propertyModel.IdOwner = 0; // Get ownerId from token
            return new JsonResult(_propertiesService.UpdateProperty(propertyModel));
        }

        [HttpPost]
        [Route("DeleteProperty/{propertyId}")]
        public JsonResult DeleteProperty(int propertyId)
        {
            int ownerId = 0; // Get ownerId from token
            return new JsonResult(_propertiesService.DeleteProperty(propertyId, ownerId));
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult Add()
        {
            return new JsonResult(string.Empty);
        }
    }
}
