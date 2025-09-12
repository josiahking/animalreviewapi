using AnimalReviewApp.Models;

namespace AnimalReviewApp.Interfaces
{
    public interface IReviewerInterface
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool Save();
        bool UpdateReviewer(Reviewer reviewer);
    }
}
