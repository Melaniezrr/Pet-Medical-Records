using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Models
{
    public class ClinicCreate
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
