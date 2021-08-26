using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Models
{
    public class PetCreate
    {
        [Required]
        public string Name { get; set; }
        public Guid? OwnerId { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
