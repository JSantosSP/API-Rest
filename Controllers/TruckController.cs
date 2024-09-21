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
            if (truck == null)
            {
                var response = new
                {
                    Message = $"The truck with ID '{id}' was not found."
                };
                return NotFound(response);
            }

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
                    Message = $"The fleet with ID '{truck.fleetId}' was not found."
                };
                return NotFound(response);
            }
            DataStore.Trucks.Add(truck);
            return CreatedAtAction(nameof(GetById),new { id = truck.id }, truck);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Truck> Put(Truck truck)
        {
            var truckOld = DataStore.Trucks.FirstOrDefault(t => t.id == truck.id);
            if (truckOld == null)
            {
                var response = new
                {
                    Message = $"The truck with ID '{truck.id}' was not found."
                };
                return NotFound(response);
            }
            var fleet = DataStore.Fleets.FirstOrDefault(f => f.id == truck.fleetId);
            if (fleet == null)
            {
                var response = new
                {
                    Message = $"The fleet with ID '{truck.fleetId}' was not found."
                };
                return NotFound(response);
            }

            truckOld.ubication = truck.ubication;
            truckOld.state = truck.state;
            truckOld.fleetId = truck.fleetId;

            var result = new
            {
                Truck = DataStore.Trucks.FirstOrDefault(f => f.id == truck.id)
            };
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(String id)
        {
            var truck = DataStore.Trucks.FirstOrDefault(t => t.id == id);
            if (truck == null)
            {
                var response = new
                {
                    Message = $"The truck with ID '{id}' was not found."
                };
                return NotFound(response);
            }
            DataStore.Trucks.Remove(truck);
            return NoContent();
        }
    }
}
