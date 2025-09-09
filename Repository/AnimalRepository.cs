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
    }
}
