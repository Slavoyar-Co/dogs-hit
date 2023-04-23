namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public int MapColor { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}