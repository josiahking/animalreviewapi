using AnimalReviewApp.Models;

namespace AnimalReviewApp.Interfaces
{
    public interface ICategoryInterface
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Animal> GetAnimalByCategory(int categoryId);
        bool CategoryExists(int id);
    }
}
