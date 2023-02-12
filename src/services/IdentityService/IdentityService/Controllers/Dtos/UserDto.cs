namespace IdentityService.Controllers.Dtos
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string? Email { get; set; }

    }
}
