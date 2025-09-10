using AnimalReviewApp.Models;

namespace AnimalReviewApp.Interfaces
{
    public interface IAnimalInterface
    {
        ICollection<Animal> GetAnimals();
        Animal GetAnimal(int id);
        Animal GetAnimal(string name);
        decimal GetAnimalRating(int id);
        bool AnimalExists(int id);
        bool CreateAnimal(int ownerId, int categoryId, Animal animal);
        bool Save();
    }
}
