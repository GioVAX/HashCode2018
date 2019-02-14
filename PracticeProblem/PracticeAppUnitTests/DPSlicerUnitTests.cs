using PracticeApp;
using Xunit;

namespace PracticeAppUnitTests
{
    // ReSharper disable once InconsistentNaming
    public class DPSlicerUnitTests
    {
        private readonly DPPizzaSlicer _sut;

        public DPSlicerUnitTests()
        {
            var pizza = new PizzaDescription(PizzaCases.Tiny);
            _sut = new DPPizzaSlicer(pizza);
        }

        //[Fact]
        public void WhenInitialized_ShouldContainWhereSlicesBeginAndEnd()
        {

        }
    }
}
