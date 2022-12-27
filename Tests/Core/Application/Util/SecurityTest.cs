using Application.Util;
using Domain.Dto.Result;

namespace Tests.Core.Application.Util
{
    [TestFixture]
    public class SecurityTest
    {
        [Test]
        public void Encrypt_ReturnCryptedString()
        {
            var stringTest = "test";

            var result = Security.Encrypt(stringTest);

            Assert.NotNull(result);
            Assert.That(result, Is.Not.EqualTo(stringTest));
            Assert.That(result, Is.EqualTo("GVXRpnXl5xo8y/avkbtwHQueO6hjsIsjdgLxzXsmE7A="));
        }

        [Test]
        public void GenerateToken_ReturnString()
        {
            var userEntity = new UserDto()
            {
                Name = "User Test",
                UserName = "usertest",
                Password = "test",
            }.ToEntity();

            var result = Security.GenerateToken(userEntity);

            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<string>());
        }
    }
}