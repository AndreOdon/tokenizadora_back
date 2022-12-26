using Domain.Dto;
using Domain.Dto.Result;
using Infra.DataBase.Repository;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driven.Infra.DataBase.Repository
{
    [TestFixture]
    public class UserRepositoryTest : DataBaseMockTest
    {
        private UserRepository _repository;

        [SetUp]
        public async Task SetUp()
        {
            _repository = new UserRepository(GetContext());

            var userEntity = new UserDto()
            {
                Name = "User Test",
                UserName = "usertest",
                Password = "test",
            }.ToEntity();

            await _repository.AddAsync(userEntity);
        }

        [TestCase("USERTEST")]
        [TestCase("usertest")]
        [TestCase("UserTest")]
        [TestCase("Usertest")]
        public async Task GetByUserName_ExistindUserName_ReturnEntity(string userName)
        {
            var entity = await _repository.GetByUserName(userName);

            Assert.NotNull(entity);
            Assert.That(entity.UserName, Is.EqualTo("usertest"));
            Assert.That(entity.Name, Is.EqualTo("User Test"));
            Assert.That(entity.Password, Is.EqualTo("test"));
        }

        [TestCase("USERTESTe")]
        [TestCase("userteste")]
        [TestCase("UserTeste")]
        [TestCase("Userteste")]
        public async Task GetByUserName_NotExistindUserName_ReturnNull(string userName)
        {
            var entity = await _repository.GetByUserName(userName);

            Assert.IsNull(entity);
        }
    }
}