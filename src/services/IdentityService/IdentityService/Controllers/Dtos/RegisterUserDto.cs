using System.Text.Json.Serialization;

namespace IdentityService.Controllers.Dtos
{
    [Serializable]
    public record RegisterUserDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string? Email { get; set; }
    }
}
