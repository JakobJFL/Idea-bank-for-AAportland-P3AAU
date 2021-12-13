using Xunit;

namespace Testing
{
    [CollectionDefinition("Test Database", DisableParallelization = true)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>{}
}