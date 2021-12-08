using Xunit;

namespace XUnitTesting
{
    [CollectionDefinition("Test Database")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>{}
}