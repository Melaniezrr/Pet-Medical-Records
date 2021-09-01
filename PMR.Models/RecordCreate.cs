using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Models
{
    public class RecordCreate
    {
        public Guid OwnerId { get; set; }
        public int PetId { get; set; }
        public int ClinicId { get; set; }
        [Required]
        public string VaccineName { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
