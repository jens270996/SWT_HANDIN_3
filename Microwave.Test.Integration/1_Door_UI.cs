
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class Door_UI
    {
        private IDoor door;
        private ILight light;
        [SetUp]
        public void Setup()
        {
            door = new Door();
            var pB = Substitute.For<IButton>();
            var tB = Substitute.For<IButton>();
            var scB = Substitute.For<IButton>();
            var disp = Substitute.For<IDisplay>();
            var cooker = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();


            UserInterface ui = new UserInterface(pB, tB, scB, door, disp, light, cooker);
        }

        [Test]
        public void DoorOpen_LightTurnOnCalled()
        {
            door.Open();

            light.Received(1).TurnOn();
        }

        [Test]
        public void DoorOpenDoorClose_LightTurnOffCalled()
        {
            door.Open();
            door.Close();
            light.Received(1).TurnOff();
        }
        [Test]
        public void DoorClose_LightTurnOffNotCalled()
        {
            
            door.Close();
            light.Received(0).TurnOff();
        }
    }
}