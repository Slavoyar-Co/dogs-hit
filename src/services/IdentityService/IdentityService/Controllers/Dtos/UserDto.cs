namespace IdentityService.Controllers.Dtos
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

    }
}
