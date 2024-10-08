﻿using APIRest.Models;
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
            if (fleet == null)
            {
                var response = new
                {
                    Message = $"The fleet with ID '{id}' was not found."
                };
                return NotFound(response);
            }
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
            var fleetLast = DataStore.Fleets.Last();
            var nId = int.Parse(fleetLast.id.Replace('F', ' '));
            fleet.id = $"F{nId + 1}";
            
            DataStore.Fleets.Add(fleet);
            return CreatedAtAction(nameof(GetById), new { id = fleet.id }, fleet);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Fleet> Put(Fleet fleet)
        {
            var fleetOld = DataStore.Fleets.FirstOrDefault(f => f.id == fleet.id);
            if(fleetOld == null)
            {
                var response = new
                {
                    Message = $"The fleet with ID '{fleet.id}' was not found."
                };
                return NotFound(response);
            }
            
            fleetOld.company = fleet.company;

            var result = new
            {
                Fleet = DataStore.Fleets.FirstOrDefault(f => f.id == fleet.id)
            };
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(String id)
        {
            var fleet = DataStore.Fleets.FirstOrDefault(f => f.id == id);
            if (fleet == null)
            {
                var response = new
                {
                    Message = $"The fleet with ID '{id}' was not found."
                };
                return NotFound(response);
            }
            var trucks = DataStore.Trucks.Where(t => t.fleetId == id).ToList();
            foreach(Truck truck in trucks)
            {
                DataStore.Trucks.Remove(truck);
            }
            DataStore.Fleets.Remove(fleet);
            return NoContent();
        }
    }
}
