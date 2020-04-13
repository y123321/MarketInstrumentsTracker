using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketsTracker.Common;
using MarketsTracker.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MarketsTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _configuration;
        private readonly IUrlHelper _urlHelper;

        public LoginController(IUsersService usersService,IConfiguration configuration,IUrlHelper urlHelper)
        {
            _usersService = usersService;
            _configuration = configuration;
            _urlHelper = urlHelper;
        }
        [HttpGet(Name = "Login")]
        public async Task<IActionResult> Login()
        {
            var emptyModel = new LoginRequest();
            return Ok(emptyModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _usersService.Authenticate(loginRequest.UserName, loginRequest.Password);
            if (user == null)
                return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = AuthenticationHelper.GetSecret(_configuration);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience= "Audience",
                Issuer ="Issuer",
                IssuedAt=DateTime.Now,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var res = new LoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Redirect = _urlHelper.Link("GetUser", new { id=user.UserId})
            };
            return Ok(res);
        } 
    }
}