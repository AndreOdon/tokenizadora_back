using Application.Services;
using Application.Util;
using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces.Adapters.Infra.DataBase;
using Domain.Interfaces.Core.Application;
using NSubstitute;

namespace Tests.Core.Application.Service
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _userService;
        private IUserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository);
        }

        [Test]
        public async Task Login_NotExistingUserName_ReturnsNull()
        {
            var inputDto = new UserLoginInputDto()
            {
                User = "userTest",
                Password = "test"
            };

            _userRepository.GetByUserName(inputDto.User).Returns(Task.FromResult<User?>(null));

            var result = await _userService.Login(inputDto);

            Assert.IsNull(result);
            await _userRepository.Received(1).GetByUserName(inputDto.User);
        }

        [Test]
        public async Task Login_ExistingUserName_WrongPassword_ReturnsNull()
        {
            var inputDto = new UserLoginInputDto()
            {
                User = "userTest",
                Password = "test"
            };

            var entity = new UserDto()
            {
                Name = "User Test",
                Password = Security.Encrypt("password"),
                UserName = "userTest"
            }.ToEntity();

            _userRepository.GetByUserName(inputDto.User).Returns(Task.FromResult<User?>(entity));

            var result = await _userService.Login(inputDto);

            Assert.IsNull(result);
            await _userRepository.Received(1).GetByUserName(inputDto.User);
        }

        [TestCase("password")]
        [TestCase("PASSWORD")]
        [TestCase("Password")]
        public async Task Login_ExistingUserName_RightPassword_ReturnsDto(string password)
        {
            var inputDto = new UserLoginInputDto()
            {
                User = "userTest",
                Password = password
            };

            var entity = new UserDto()
            {
                Name = "User Test",
                Password = Security.Encrypt("password"),
                UserName = "userTest"
            }.ToEntity();

            _userRepository.GetByUserName(inputDto.User).Returns(Task.FromResult<User?>(entity));

            var result = await _userService.Login(inputDto);

            await _userRepository.Received(1).GetByUserName(inputDto.User);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<UserDto>());
            Assert.That(result.Name, Is.EqualTo(entity.Name));
            Assert.That(result.UserName, Is.EqualTo(entity.UserName));
            Assert.That(result.Token, Is.TypeOf<string>());
            Assert.That(result.Token, Is.Not.Empty);
        }
    }
}