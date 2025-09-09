namespace AnimalReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AnimalCategory> AnimalCategories { get; set; }
    }
}
