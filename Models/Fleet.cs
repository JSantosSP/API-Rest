using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIRest.Models
{
    public class Fleet
    {
        public String id { get;  set; }
        [Required(ErrorMessage = "Company name is required")]
        public String company { get;  set; }
    }
}
