namespace AnimalReviewApp.Models
{
    public class AnimalOwner
    {
        public int AnimalId { get; set; }
        public int OwnerId { get; set; }
        public Animal Animal { get; set; }
        public Owner Owner { get; set; }
    }
}
