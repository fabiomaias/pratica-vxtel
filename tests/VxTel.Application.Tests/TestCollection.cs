using Xunit;

namespace VxTel.Application.Tests
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection : ICollectionFixture<TestsFixtures> { }
}
