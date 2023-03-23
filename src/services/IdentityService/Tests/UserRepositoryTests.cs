
namespace Tests
{
    public class UserRepositoryTests
    {
        private DbContextOptions<IdentityDbContext> _dbContextOptions;
        private IdentityDbContext _dbContext;
        private UserRepository _userRepository;

        public UserRepositoryTests(DbContextOptions<IdentityDbContext> dbContextOptions)
        {
            var dbName = $"AuthorPostsDb_{DateTime.Now.ToFileTimeUtc()}";
            _dbContextOptions = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            _dbContext = new IdentityDbContext(_dbContextOptions);
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async void UserRepository_RegisterUserAsync_Success()
        {
            var status = await _userRepository.RegisterUserAsync(new User { 
                Name = "TestName",
                CreateTime = DateTime.UtcNow,
                Email = "TestEmail",
                Login = "TestLogin",
                Password = "TestPassword"
            });

            Assert.Equal(ERegistrationStatus.Success, status);
        }
    }
}