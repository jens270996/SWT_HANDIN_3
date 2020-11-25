using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Door_UI
    {
        [SetUp]
        public void Setup()
        {

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}