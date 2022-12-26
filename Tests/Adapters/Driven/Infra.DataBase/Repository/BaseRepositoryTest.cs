using Domain.Dto.Result;
using Domain.Entities;
using Infra.DataBase.Repository;
using Tests.Adapters.Driven.Infra.DataBase.ContextMock;

namespace Tests.Adapters.Driven.Infra.DataBase.Repository
{
    [TestFixture]
    public class BaseRepositoryTest : DataBaseMockTest
    {
        private BaseRepository<User> _repository;
        private int _userId;

        [SetUp]
        public async Task SetUp()
        {
            _repository = new BaseRepository<User>(GetContext());

            var userEntity = new UserDto()
            {
                Name = "User Test",
                UserName = "usertest",
                Password = "test",
            }.ToEntity();

            await _repository.AddAsync(userEntity);

            _userId = userEntity.Id;
        }

        [Test]
        public async Task GetById_ExistingId_ReturnUser()
        {
            var result = await _repository.GetById(_userId);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<User>());
            Assert.That(result.Id, Is.EqualTo(_userId));
            Assert.That(result.Name, Is.EqualTo("User Test"));
            Assert.That(result.UserName, Is.EqualTo("usertest"));
            Assert.That(result.Password, Is.EqualTo("test"));
        }

        [Test]
        public async Task GetById_NotExistingId_ReturnNull()
        {
            var result = await _repository.GetById(0);

            Assert.IsNull(result);
        }

        [Test]
        public async Task SaveAsync_ReturnSavedEntityId()
        {
            var newEntity = new UserDto()
            {
                Name = "New User Test",
                UserName = "newUsertest",
                Password = "newtest",
            }.ToEntity();

            Assert.IsNotNull(newEntity.Id);
            Assert.That(newEntity.Id, Is.EqualTo(0));

            await _repository.AddAsync(newEntity);

            Assert.IsNotNull(newEntity.Id);
            Assert.That(newEntity.Id, Is.Not.EqualTo(0));
        }
    }
}