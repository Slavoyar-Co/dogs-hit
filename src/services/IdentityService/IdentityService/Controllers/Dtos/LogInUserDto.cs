namespace IdentityService.Controllers.Dtos
{
    [Serializable]
    public record LogInUserDto
    {
        public string Password { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string? Email { get; set; }
    }
}
