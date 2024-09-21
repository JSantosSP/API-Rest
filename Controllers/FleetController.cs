using APIRest.Models;
using APIRest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FleetController : ControllerBase
    {

        private readonly ILogger<FleetController> _logger;

        public FleetController(ILogger<FleetController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Fleet> Get()
        {
            return DataStore.Fleets;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(String id)
        {
            var fleet = DataStore.Fleets.FirstOrDefault(f => f.id == id);
            if (fleet == null) return NotFound();
            var trucks = DataStore.Trucks.Where(t => t.fleetId == id).ToList();

            var result = new
            {
                Fleet = fleet,
                Trucks = trucks
            };
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Fleet> Post(Fleet fleet)
        {
            var size = DataStore.Fleets.Count;
            fleet.id = $"F{size + 1}";
            
            DataStore.Fleets.Add(fleet);
            return CreatedAtAction(nameof(GetById), new { id = fleet.id }, fleet);
        }
    }
}
