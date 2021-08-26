using PMR.Data;
using PMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Services
{
    public class RecordService
    {
        private readonly Guid _userId;

        public RecordService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRecord(RecordCreate model)
        {

        var entity = new Record()
            {
                //PetId = model.PetId,
                //ClinicId = model.ClinicId,
                VaccineName = model.VaccineName,
                Date = model.Date,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Records.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RecordListItem> GetRecords()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Records

                    .Select(e => new RecordListItem
                        {
                            RecordId = e.RecordId,
                            VaccineName = e.VaccineName,
                            Date = e.Date,

                        }
                    );

                return query.ToArray();
            }
        }
    }
}
