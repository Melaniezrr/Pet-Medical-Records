using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Models
{
    public class RecordEdit
    {
        public int RecordId { get; set; }
        public Guid OwnerId { get; set; }
        public int PetId { get; set; }
        public int ClinicId { get; set; }
        public string VaccineName { get; set; }
        public string Date { get; set; }
    }
}
