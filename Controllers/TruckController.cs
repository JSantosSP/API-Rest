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
    public class TruckController : ControllerBase
    {

        private readonly ILogger<TruckController> _logger;

        public TruckController(ILogger<TruckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Truck> Get()
        {
            return DataStore.Trucks;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(String id)
        {
            var truck = DataStore.Trucks.FirstOrDefault(t => t.id == id);
            if (truck == null) return NotFound();

            var result = new
            {
                Truck = truck
            };
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Truck> Post(Truck truck)
        {
            var size = DataStore.Trucks.Count;
            truck.id = $"T{size + 1}";
            var fleet = DataStore.Fleets.FirstOrDefault(f => f.id == truck.fleetId);
            if (fleet == null)
            {
                var response = new
                {
                    Message = $"La flota con ID '{truck.fleetId}' no se encontró."
                };
                return NotFound(response);
            }
            DataStore.Trucks.Add(truck);
            return CreatedAtAction(nameof(GetById),new { id = truck.id }, truck);
        }
    }
}
