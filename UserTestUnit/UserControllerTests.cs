using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using UserApplication.ServiceInterfaces;
using UserDomain;
using UserWebApi.Controllers;

namespace UserTestUnit
{
    public class UserControllerTests
    {
        private readonly IUserService _userService = Substitute.For<IUserService>();
        private readonly UsersController _sut;

        public UserControllerTests()
        {
            _sut = new UsersController(_userService);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _userService.GetAll().Returns(new List<User>());

            // Act
            var result = (OkObjectResult)await _sut.GetAll();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<List<User>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetAll_ShouldReturnUsersResponse_WhenUsersExist()
        {
            // Arrange
            User user1 = new User
            {
                Id = 2,
                FirstName = "Paria",
                LastName = "Ham",
            };
            User user2 = new User
            {
                Id = 2,
                FirstName = "Samira",
                LastName = "Ek",
            };
            var users = new List<User> { user1, user2 };
            _userService.GetAll().Returns(users);

            // Act
            var result = (OkObjectResult)await _sut.GetAll();

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<List<User>>().Should().BeEquivalentTo(users);
        }

        [Fact]
        public async void GetById_ReturnNotFound_WhenUserDoesntExists()
        {
            // Arrange
            _userService.GetById(Arg.Any<int>()).ReturnsNull();

            // Act
            var random = new Random();
            var result = (NotFoundResult)await _sut.GetById(random.Next());

            // Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetById_ReturnOkAndObject_WhenUserExists()
        {
            // Arrange
            User user = new User
            {
                Id = 2,
                FirstName = "Paria",
                LastName = "Ham",
            };
            _userService.GetById(user.Id).Returns(user);

            // Act
            var result = (OkObjectResult)await _sut.GetById(user.Id);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(user);
        }
    }
}
