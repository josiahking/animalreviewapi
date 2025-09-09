using AnimalReviewApp.Data;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;

namespace AnimalReviewApp.Repository
{
    public class CategoryRepository : ICategoryInterface
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Animal> GetAnimalByCategory(int categoryId)
        {
            return _context.AnimalCategories.Where(a => a.CategoryId == categoryId).Select(a => a.Animal).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
