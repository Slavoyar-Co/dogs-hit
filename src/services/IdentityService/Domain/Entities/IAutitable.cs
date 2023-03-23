namespace Domain.Entities
{
    public interface IAuditable
    {
        public DateTime? CreateTime { get; set; }
    }
}
