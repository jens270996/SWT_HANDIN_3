
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Microwave.Test.Integration
{
    public class ButtonDriver : IButton
    {
        public event EventHandler Pressed;

        public void Press()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }
    }
    public class Door_UI
    {
        private IDoor door;
        private ILight light;
        private IButton scB;
        private IButton pB;
        private IButton tB;
        private ICookController cooker;
        [SetUp]
        public void Setup()
        {
            door = new Door();
            pB = new ButtonDriver();
            tB = new ButtonDriver();
            scB = new ButtonDriver();
            var disp = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();
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

        [Test]
        public void Power_Timer_StartButtonPressedDoorOpened_CookerStopped()
        {
            pB.Press();
            tB.Press();
            scB.Press();
            door.Open();
            cooker.Received(1).Stop();
            
        }
        
    }
}