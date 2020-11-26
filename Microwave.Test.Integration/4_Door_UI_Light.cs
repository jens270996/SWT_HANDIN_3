using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class _4_Door_UI_Light
    {
        IButton pB;
        IButton tB;
        IButton scB;
        IDoor door;
        IDisplay disp;
        IOutput output;
        ICookController cooker;
        UserInterface ui;
        ILight light;
        [SetUp]
        public void Setup()
        {
            door = new Door();
            pB = new Button();
            tB = new Button();
            scB = new Button();
            output = Substitute.For<IOutput>();
            disp = Substitute.For<IDisplay>();

            cooker = Substitute.For<ICookController>();
            light = new Light(output);


            ui = new UserInterface(pB, tB, scB, door, disp, light, cooker);
        }

        [Test]
        public void DoorOpen_LightOn()
        {
            door.Open();
            output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void DoorOpenOpen_LightOnOnlyOnce()
        {
            door.Open();
            output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void DoorClose_LightAlreadyOff()
        {
            door.Close();
            output.Received(0).OutputLine("Light is turned off");
        }

        [Test]
        public void DoorOpenDoorClose_LightOnOff()
        {
            door.Open();
            door.Close();
            output.Received(1).OutputLine("Light is turned on");
            output.Received(1).OutputLine("Light is turned off");
        }

    }
}
