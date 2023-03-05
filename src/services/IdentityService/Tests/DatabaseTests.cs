

namespace Tests
{
    public class DatabaseTests
    {
        private readonly IConfiguration _configuration;

        public DatabaseTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Fact]
        public async void Database_Can_Be_Connected_Check()
        {
            var dbName = $"AuthorPostsDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<IdentityDbContext> dbContextOptions = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseNpgsql(_configuration.GetConnectionString("defaultConnectionString"))
                .Options;

            using (IdentityDbContext dbContext = new IdentityDbContext(dbContextOptions))
            {
               var response = await dbContext.Database.CanConnectAsync();
               
                Assert.True(response);
            }
        }

    }
}
