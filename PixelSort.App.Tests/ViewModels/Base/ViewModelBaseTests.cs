using PixelSort.App.ViewModels.Base;
using Xunit;

namespace PixelSort.App.Tests.ViewModels.Base
{
    /// <summary>
    /// Test ViewModelBase functionality.
    /// </summary>
    public class ViewModelBaseTests
    {
        /// <summary>
        /// Test class deriving from ViewModelBase that will be used for test purposes.
        /// </summary>
        class ViewModel : ViewModelBase
        {
            private string _testProperty;

            public string TestProperty
            {
                get => _testProperty;
                set => SetProperty(ref _testProperty, value);
            }
        }

        /// <summary>
        /// Test that the property change fires the PropertyChanged event properly
        /// upon changing the property value.
        /// </summary>
        [Fact]
        public void SetProperty_DifferentValueNotifiesTest()
        {
            // expectations
            ViewModel vm = new ViewModel();

            string expectedPropertyName = nameof(vm.TestProperty);
            string expectedPropertyValue = "Test";
            bool actualPropertyChangedFired = false;

            vm.PropertyChanged += (sender, e) =>
            {
                actualPropertyChangedFired = (e.PropertyName.Equals(expectedPropertyName));
            };

            // act
            vm.TestProperty = expectedPropertyValue;

            // verify
            Assert.True(actualPropertyChangedFired);
            Assert.Equal(expectedPropertyValue, vm.TestProperty);
        }

        /// <summary>
        /// Test that the property changed event gets not fired
        /// when assigning the same value to the property (no change).
        /// </summary>
        [Fact]
        public void SetProperty_SameValueDoesNotNotifyTest()
        {
            // expectations
            string expectedPropertyValue = "Test";
            bool actualPropertyChangedFired = false;

            ViewModel vm = new ViewModel();
            vm.TestProperty = expectedPropertyValue;
            vm.PropertyChanged += (sender, e) =>
            {
                actualPropertyChangedFired = true;
            };

            //act
            vm.TestProperty = expectedPropertyValue;

            // verify
            Assert.False(actualPropertyChangedFired);
        }
    }
}
