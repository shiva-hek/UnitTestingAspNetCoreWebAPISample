using Microsoft.EntityFrameworkCore;
using UserDataAccess;
using UserDomain;

namespace UserIntergrationTest
{
    public class UserApiFactory : IDisposable
    {
        public UserDbContext ApplicationDbContext { get; private set; }
        public UserApiFactory()
        {
            CreateFakeUser();
        }
      
        private void CreateFakeUser()
        {
            SetServices();
            User userAlpha = new User
            {
                Id = 1,
                FirstName = "Alz",
                LastName = "Najafi",
            };
            User userBeta = new User
            {
                Id = 2,
                FirstName = "Reza",
                LastName = "Shiva",
            };
            ApplicationDbContext.Users.Add(userAlpha);
            ApplicationDbContext.Users.Add(userBeta);
            ApplicationDbContext.SaveChanges();
        }

        private void SetServices()
        {
            string dbName = $"CruDTestDb_{DateTime.Now.ToFileTimeUtc()}";
            DbContextOptions<UserDbContext> dbContextOptions = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            ApplicationDbContext = new UserDbContext(dbContextOptions);
        }

        public void Dispose()
        {
            ApplicationDbContext.Dispose();
        }
    }
}
