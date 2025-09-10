using AnimalReviewApp.Models;

namespace AnimalReviewApp.Interfaces
{
    public interface IReviewInterface
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfAnimal(int animalId);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool Save();
    }
}
