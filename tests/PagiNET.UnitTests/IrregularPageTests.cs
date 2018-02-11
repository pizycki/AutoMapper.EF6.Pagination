using Ploeh.AutoFixture;
using Xunit;

namespace PagiNET.UnitTests
{
    /// <summary>
    /// Tests for pagination with unfixed parameters.
    /// </summary>
    public class IrregularPageTests
    {
        [Fact]
        public void can_take_range_of_items_starting_from_specific_one()
        {
            var itemsCount = 30;
            var items = new Fixture().CreateMany<Foo>(itemsCount);


        }
    }

    public class Foo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
