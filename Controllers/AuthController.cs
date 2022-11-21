using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDASFeedbackTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class CheckTokenArgs
        {
            public Guid token { get; set; }
            public string url { get; set; }
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserName")]
        public ActionResult<string> GetUserName()
        {
            if (User == null)
                return Unauthorized();
            var name = ControllerHelperFunctions.GetUserNameFromClaim(User);
            return Ok(name);

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetIDASApiUrl")]
        public ActionResult<string> GetIDASApiUrl()
        {
            string url = "https://api.dev.idas-cloudservices.net/api/"; //Environment.GetEnvironmentVariable("API_ENVIRONMENT") ?? _configuration.GetValue<string>("API_ENVIRONMENT");

            if (string.IsNullOrEmpty(url))
                return NotFound();

            return Ok(url);
        }

        [HttpGet]
        [Authorize]
        [Route("IsAdmin")]
        public ActionResult<bool> IsAdmin()
        {
            if (User == null)
                return Unauthorized();

            return ControllerHelperFunctions.CheckPermissionCode(User, "FeedbackAdmin");
        }
    }
}