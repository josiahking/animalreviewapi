namespace AnimalReviewApp.Models
{
    public class AnimalCategory
    {
        public int AnimalId { get; set; }
        public int CategoryId { get; set; }
        public Animal Animal { get; set; }
        public Category Category { get; set; }
    }
}
