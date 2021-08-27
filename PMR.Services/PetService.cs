using PMR.Data;
using PMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR.Services
{
    public class PetService
    {
        private readonly Guid _userId;

        public PetService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePet(PetCreate model)
        {
            var entity = new Pet()
            {
                OwnerId = _userId,
                Name = model.Name,
                Age = model.Age,
                Type = model.Type
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PetListItem> GetPets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Pets
                    .Where(e => e.OwnerId == _userId)
                    .Select(e => new PetListItem
                        {
                            PetId = e.PetId,
                            Name = e.Name,
                            Age = e.Age,
                            Type = e.Type
                        }
                    );

                return query.ToArray();
            }
        }

        public PetDetail GetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Pets
                    .Single(e => e.PetId == id && e.OwnerId == _userId);
                return new PetDetail
                {
                    PetId = entity.PetId,
                    Name = entity.Name,
                    OwnerId = entity.OwnerId,
                    Age = entity.Age,
                    Type = entity.Type
                };
            }
        }

        public bool UpdatePet(PetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Pets
                    .Single(e => e.PetId == model.PetId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.OwnerId = model.OwnerId;
                entity.Age = model.Age;
                entity.Type = model.Type;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
