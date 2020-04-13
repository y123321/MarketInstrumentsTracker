using System.Threading.Tasks;
using MarketsTracker.Common;
using MarketsTracker.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketsTracker.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IInstrumentsService _instrumentsService;
        private readonly IUrlHelper _urlHelper;

        public UsersController(IUsersService usersService, IInstrumentsService instrumentsService,IUrlHelper urlHelper)
        {
            _usersService = usersService;
            _instrumentsService = instrumentsService;
            _urlHelper = urlHelper;
        }
      

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return UnprocessableEntity("invalid id");
            if (id.ToString() != User.Identity.Name)
                return Forbid();
            var user = await _usersService.Get(id);
            if (user == null)
                return NotFound();

            return Ok(user);

        }

        // POST: api/Users
        [HttpPost(Name = "Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest user)
        {
            var passwordValidationRes = AuthenticationHelper.CheckPasswordRequierments(user.Password);
            if (passwordValidationRes != null)
                return BadRequest(passwordValidationRes);

            if (await _usersService.IsUserExists(user.UserName))
                return UnprocessableEntity($"user name {user.UserName} is taken");

            await _usersService.Register(user);
            var redirectUrl = _urlHelper.Link("Login", null);
            return Redirect(redirectUrl);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (user.UserId != id)
                return UnprocessableEntity();
            if (id.ToString() != User.Identity.Name)
                return Forbid();
            await _usersService.Update(user);
            return Ok();

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return RedirectToAction("Get", new { id = User.Identity.Name });
        }
    }
}
