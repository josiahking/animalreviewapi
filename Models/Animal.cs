namespace AnimalReviewApp.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<AnimalOwner> AnimalOwners { get; set; }
        public ICollection<AnimalCategory> AnimalCategories { get; set; }

    }
}
