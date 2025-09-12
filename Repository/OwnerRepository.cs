using AnimalReviewApp.Data;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;

namespace AnimalReviewApp.Repository
{
    public class OwnerRepository : IOwnerInterface
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Animal> GetAnimalByOwner(int ownerId)
        {
            return _context.AnimalOwners.Where(a => a.Owner.Id == ownerId)
                .Select(a => a.Animal).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAnimal(int animalId)
        {
            return _context.AnimalOwners.Where(ao => ao.Animal.Id == animalId)
                .Select(o => o.Owner).ToList();
        }

        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(o => o.Id == id);
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
