using Xunit;

namespace Testing
{
    [CollectionDefinition("Test Database")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>{}
}