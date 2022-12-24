using Infra.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Tests.Adapters.Driven.Infra.DataBase.ContextMock
{
    public class DataBaseMockTest
    {
        public DatabaseContext GetContext()
        {
            var option = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: "Test_Tokenizadora").Options;

            var context = new DatabaseContext(option);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}