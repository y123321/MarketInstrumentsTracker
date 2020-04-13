using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketsTracker.Common;
using MarketsTracker.Model;
using MarketsTracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MarketsTracker.Controllers
{
    [Route("api/Instruments")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InstrumentsController : ControllerBase
    {
        private readonly IInstrumentsService _instrumentsService;

        public InstrumentsController(IInstrumentsService instrumentsService)
        {
            _instrumentsService = instrumentsService;
        }
        // GET: api/Instruments
        [HttpGet(Name = "GetInstruments")]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery]int amount)
        {
       
            ICollection<Instrument> instruments = await _instrumentsService.GetAllInstruments(page,amount);
            return Ok(instruments);
        }

        // GET: api/Instruments/5
        [HttpGet("{id}", Name = "GetInstrument")]
        public async Task<IActionResult> Get(int id, [FromQuery] int page,[FromQuery]int amount)
        {
            Instrument instrument = await _instrumentsService.GetInstrument(id,page,amount);
            return Ok(instrument);
        }
        
    }
}
