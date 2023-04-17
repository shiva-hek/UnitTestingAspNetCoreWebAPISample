using FluentAssertions;
using NSubstitute;
using UserApplication.ServiceInterfaces;
using UserApplication.Services;
using UserDomain;
using UserDomain.Repository;

namespace UserTestUnit
{
    public class UserServiceTests
    {
        private readonly IUserService _sut;
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

        public UserServiceTests()
        {
            _sut = new UserService(_userRepository);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _userRepository.GetAllAsync().Returns(new List<User>());

            // Act
            var result = _sut.GetAll();

            // Assert
            result.Result.Should().BeEmpty();
        }

        [Fact]
        public void GetAll_ShouldReturnUsers_WhenSomeUsersExist()
        {

            // Arrange
            User user1 = new User
            {
                Id = 2,
                FirstName = "Shiva",
                LastName = "Hek",
            };
            User user2 = new User
            {
                Id = 2,
                FirstName = "Samira",
                LastName = "Ek",
            };
            User user3 = new User
            {
                Id = 2,
                FirstName = "Samane",
                LastName = "Sal",
            };
            List<User> expected = new List<User> { user1, user2 };
            _userRepository.GetAllAsync().Returns(expected);

            // Act
            var result = _sut.GetAll();

            // Assert
            result.Result.Should().BeEquivalentTo(expected);
            result.Result.Should().ContainEquivalentOf(user1);
            result.Result.Should().ContainEquivalentOf(user2);
            result.Result.Should().NotContainEquivalentOf(user3);
        }

        [Fact]
        public void GetById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 1,
                FirstName = "Paria",
                LastName = "Ham",
            };
            _userRepository.GetAsync(existingUser.Id).Returns(existingUser);

            // Act
            var result = _sut.GetById(existingUser.Id);

            // Assert
            result.Result.Should().BeEquivalentTo(existingUser);
        }

    }
}
