using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIRest.Models
{
    public class Truck
    {
        public String id { get; set; }
        [Required(ErrorMessage = "Ubication is required")]
        public String ubication { get; set; }
        [Required(ErrorMessage = "State is required")]
        public String state { get; set; }
        [Required(ErrorMessage = "Fleet id is required")]
        public String fleetId { get; set; }
    }
}
