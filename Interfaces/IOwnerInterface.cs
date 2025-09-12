using AnimalReviewApp.Models;

namespace AnimalReviewApp.Interfaces
{
    public interface IOwnerInterface
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        ICollection<Owner> GetOwnerOfAnimal(int animalId);
        ICollection<Animal> GetAnimalByOwner(int ownerId);
        bool OwnerExists(int id);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool Save();
    }
}
