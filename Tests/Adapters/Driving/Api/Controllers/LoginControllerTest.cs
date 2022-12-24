using API.Controllers;
using Application.Services;
using Application.Util;
using Domain.Dto;
using Infra.DataBase.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driving.Api.Controllers
{
    [TestFixture]
    public class LoginControllerTest : DataBaseMockTest
    {
        private LoginController _loginController;

        [SetUp]
        public async Task SetUp()
        {
            var userRepository = new UserRepository(GetContext());

            var userEntity = new UserDto()
            {
                Name = "User Test",
                UserName = "usertest",
                Password = Security.Encrypt("password"),
            }.ToEntity();

            await userRepository.AddAsync(userEntity);
            await userRepository.SaveChangesAsync();

            var userService = new UserService(userRepository);
            _loginController = new LoginController(userService);
        }

        [TestCase("usertest", "password")]
        [TestCase("USERTEST", "PASSWORD")]
        [TestCase("Usertest", "Password")]
        public async Task Login_ExistingUserAndPassword_ReturnOk(string user, string password)
        {
            var inputDto = new UserLoginInputDto()
            {
                User = user,
                Password = password
            };

            var actionResult = await _loginController.Login(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<UserDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<OkObjectResult>());

            var result = actionResult.Result as OkObjectResult;
            var resultValue = result.Value as UserDto;
            Assert.That(resultValue.Name, Is.EqualTo("User Test"));
            Assert.That(resultValue.UserName, Is.EqualTo("usertest"));
            Assert.That(resultValue.Token, Is.TypeOf<string>());
            Assert.That(resultValue.Token, Is.Not.Empty);
        }

        [Test]
        public async Task Login_NotExistingUserAndPassword_ReturnBadRequest()
        {
            var inputDto = new UserLoginInputDto()
            {
                User = "newUser",
                Password = "password"
            };

            var actionResult = await _loginController.Login(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<UserDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Login_ExistingUserAndWorngPassword_ReturnBadRequest()
        {
            var inputDto = new UserLoginInputDto()
            {
                User = "usertest",
                Password = "test"
            };

            var actionResult = await _loginController.Login(inputDto);

            Assert.NotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf<ActionResult<UserDto>>());
            Assert.That(actionResult.Result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}
