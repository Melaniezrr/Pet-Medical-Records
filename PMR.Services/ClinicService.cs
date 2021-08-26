using PMR.Data;
using PMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Services
{
    public class ClinicService
    {
        private readonly Guid _userId;

        public ClinicService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateClinic(ClinicCreate model)
        {
            var entity = new Clinic()
            {
                Name = model.Name,
                Address = model.Address,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Clinics.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ClinicListItem> GetClinics()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Clinics
                    .Select(e => new ClinicListItem
                        {
                            ClinicId = e.ClinicId,
                            Name = e.Name,
                            Address = e.Address,
 
                        }
                    );

                return query.ToArray();
            }
        }
    }
}
