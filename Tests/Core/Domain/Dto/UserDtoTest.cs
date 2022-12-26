using Domain.Dto.Result;
using Domain.Entities;

namespace Tests.Core.Domain.Dto
{
    [TestFixture]
    public class UserDtoTest
    {
        [Test]
        public void ToEntity_ReturnsValidUserEntity()
        {
            var dto = new UserDto()
            {
                Name = "User Test",
                UserName = "userTest",
                Password = "Test"
            };

            var entity = dto.ToEntity();

            Assert.IsNotNull(entity);
            Assert.That(entity, Is.TypeOf<User>());
            Assert.That(entity.Name, Is.EqualTo(dto.Name));
            Assert.That(entity.UserName, Is.EqualTo(dto.UserName));
            Assert.That(entity.Password, Is.EqualTo(dto.Password));
        }

        [Test]
        public void FromEntity_ReturnsValidUserDto_WithToken()
        {
            var entity = new UserDto()
            {
                Name = "User Test",
                UserName = "userTest",
                Password = "Test"
            }.ToEntity();

            var dto = UserDto.FromEntity(entity, "tokenTest");

            Assert.IsNotNull(dto);
            Assert.That(dto, Is.TypeOf<UserDto>());
            Assert.That(dto.Name, Is.EqualTo(entity.Name));
            Assert.That(dto.UserName, Is.EqualTo(entity.UserName));
            Assert.That(dto.Id, Is.EqualTo(entity.Id));
            Assert.That(dto.Token, Is.EqualTo("tokenTest"));
        }
    }
}