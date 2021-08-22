using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Data
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }
        public Guid OwnerId { get; set; }
        [ForeignKey(nameof(Pet))]
        public int PetId { get; set; }
        [ForeignKey(nameof(Clinic))]
        public int ClinicId { get; set; }

        [Required]
        public string VaccineName { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
