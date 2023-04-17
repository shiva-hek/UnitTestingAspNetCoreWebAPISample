using FluentAssertions;
using UserApplication.ServiceInterfaces;
using UserApplication.Services;
using UserDataAccess;
using UserDataAccess.Repository;
using UserDomain;
using UserDomain.Repository;

namespace UserIntergrationTest
{
    public class UserServiceTest : IClassFixture<UserApiFactory>
    {
        private readonly IUserService _userService;
        private readonly UserDbContext _userDbContext;
        private readonly IUserRepository _userRepository;

        public UserServiceTest(UserApiFactory userApiFactory)
        {
            _userDbContext = userApiFactory.ApplicationDbContext;
            _userRepository = new UserRepository(_userDbContext);
            _userService = new UserService(_userRepository);
        }
        [Fact]
        public void GetAll_ShouldReturnUser_WhenUserExist()
        {
            // Act
            var result = _userService.GetAll();
            // Assert
            result.Should().NotBeEmpty();
            Assert.IsType<List<User>>(result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ShouldReturnUser_WhenUserExist(int id)
        {
            // Act
            var result = _userService.GetById(id);
            // Assert
            result.Should().NotBeNull();
            Assert.IsType<User>(result);
            Assert.Equal(id, result.Id);
            result.Id.Should().Be(id);
        }

        [Theory]
        [InlineData(36)]
        [InlineData(44)]
        public void GetById_ShouldReturnNullRefernceException_WhenUserDosentExist(int id)
        {
            Action act = () => _userService.GetById(id);

            NullReferenceException ex = Assert.Throws<NullReferenceException>(act);
            Assert.NotNull(ex);
            Assert.IsType<NullReferenceException>(ex);
        }

        public static IEnumerable<object[]> InvalidUser =>
          new List<object[]>
          {
             new object[]{ new User { FirstName = "A012345678910" } },
             new object[]{new User { FirstName = "Alz"} },
          };

        [Theory]
        [MemberData(nameof(InvalidUser))]
        public void Create_ShouldReturnInvalidParameterException_WhenUserIsInvalid(User user)
        {
            // Act
            Action act = () => _userService.Add(user);
            // Assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }
        //ToDo: The below test only run in debug mode
        [Fact]
        public void Create_ShouldReturnUser_WhenUserIsAdd()
        {
            // Arrange
            User user = new User
            {
                FirstName = "Alz",
                LastName = "Bahareh"
            };

            // Act
            var result = _userService.Add(user);

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(user);
        }
    }
}
