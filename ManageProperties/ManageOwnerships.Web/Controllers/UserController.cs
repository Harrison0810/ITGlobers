using ManageOwnerships.Domain.Models;
using ManageOwnerships.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManageOwnerships.Web.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authentication api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Authenticate")]
        public JsonResult Authenticate([FromBody] AuthModel model)
        {
            return new JsonResult(_userService.Authenticate(model));
        }
    }
}
