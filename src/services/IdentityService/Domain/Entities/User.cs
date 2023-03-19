namespace Domain.Entities
{
    public record User : EntityBase, IAuditable
    {
        public string Name { get; set; } = null!; 
        public string? Email { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreateTime { get; set; }
    }
}