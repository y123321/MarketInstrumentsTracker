using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketsTracker.Common;
using MarketsTracker.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketsTracker.Controllers
{
    [Route("api/Users/{userId}/Instruments")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserInstrumentsController : ControllerBase
    {
        private readonly IInstrumentsService _instrumentsService;

        public UserInstrumentsController(IInstrumentsService instrumentsService) {
            _instrumentsService = instrumentsService;
        }
        // GET: /<controller>/
        [HttpPut]
        public async Task<IActionResult> Put(int userId, [FromBody]int[] instrumentIds)
        {
            if (instrumentIds==null)
                return BadRequest();
            if(userId<1)
                return UnprocessableEntity();
            if (userId.ToString() != User.Identity.Name)
                return Forbid();
            await _instrumentsService.UpdateUserInstruments(userId,instrumentIds);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            if (userId<1)
                return UnprocessableEntity();
            if (userId.ToString() != User.Identity.Name)
                return Forbid();
            var instruments = await _instrumentsService.GetUserInstruments(userId);
            return Ok(instruments);
        }
    }
}
