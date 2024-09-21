using APIRest.Models;
using APIRest.Utils;
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
        public IActionResult GetTruck(String id)
        {
            var truck = DataStore.Trucks.FirstOrDefault(t => t.id == id);
            if (truck == null) return NotFound();

            var result = new
            {
                Truck = truck
            };
            return Ok(result);
        }
    }
}
