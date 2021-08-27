using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Models
{
    public class PetDetail
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public int Age { get; set; }
        public string Type { get; set; }
    }
}
