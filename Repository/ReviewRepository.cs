using AnimalReviewApp.Data;
using AnimalReviewApp.Interfaces;
using AnimalReviewApp.Models;

namespace AnimalReviewApp.Repository
{
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAnimal(int animalId)
        {
            return _context.Reviews.Where(r => r.Animal.Id == animalId).ToList();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
