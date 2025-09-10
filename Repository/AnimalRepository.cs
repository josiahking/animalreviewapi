using AnimalReviewApp.Data;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;

namespace AnimalReviewApp.Repository
{
    public class AnimalRepository : IAnimalInterface
    {
        private readonly DataContext _context;

        public AnimalRepository(DataContext context)
        {
            _context = context;
        }

        public bool AnimalExists(int id)
        {
            return _context.Animals.Any(a => a.Id == id);
        }

        public bool CreateAnimal(int ownerId, int categoryId, Animal animal)
        {
            var animalOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var animalCategoryentity = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();
            var animalOwner = new AnimalOwner()
            {
                Owner = animalOwnerEntity,
                Animal = animal
            };

            _context.Add(animalOwner);

            var animalCategory = new AnimalCategory()
            {
                Category = animalCategoryentity,
                Animal = animal
            };

            _context.Add(animalCategory);
            _context.Add(animal);
            return Save();
        }

        public Animal GetAnimal(int id)
        {
            return _context.Animals.Where(a => a.Id == id).FirstOrDefault();
        }

        public Animal GetAnimal(string name)
        {
            return _context.Animals.Where(a => a.Name == name).FirstOrDefault();
        }

        public decimal GetAnimalRating(int id)
        {
            var review = _context.Reviews.Where(a => a.Animal.Id == id);
            if(review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Animal> GetAnimals()
        {
            return _context.Animals.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
