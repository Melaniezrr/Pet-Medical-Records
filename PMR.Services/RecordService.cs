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
                PetId = model.PetId,
                ClinicId = model.ClinicId,
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

        public RecordDetail GetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Records
                    .Single(e => e.RecordId == id && e.OwnerId == _userId);
                return new RecordDetail
                {
                    RecordId = entity.RecordId,
                    PetId = entity.PetId,
                    ClinicId = entity.ClinicId,
                    OwnerId = entity.OwnerId,
                    VaccineName = entity.VaccineName,
                    Date = entity.Date,
                    
                };
            }
        }

        public bool UpdateRecord(RecordEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Records
                    .Single(e => e.RecordId == model.RecordId && e.OwnerId == _userId);

                entity.PetId = entity.PetId;
                entity.ClinicId = entity.ClinicId;
                entity.VaccineName = model.VaccineName;
                entity.Date = model.Date;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRecord(int recordId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Records
                    .Single(e => e.RecordId == recordId && e.OwnerId == _userId);
                ctx.Records.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
