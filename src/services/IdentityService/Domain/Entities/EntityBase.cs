namespace Domain.Entities
{
    public abstract record EntityBase
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
