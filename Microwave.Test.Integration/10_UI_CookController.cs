using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class _10_UI_CookController
    {
        private ITimer timer;
        private ICookController cookController;
        private IPowerTube powerTube;
        private IOutput output;
        private IDisplay display;
        private IUserInterface userInterface;
        private IButton pB;
        private IButton tB;
        private IButton scB;
        private IDoor door;
        private ILight light;
        private StringWriter str;

        [SetUp]
        public void Śetup()
        {
            output = new Output();
            display = new Display(output);
            light = new Light(output);
            powerTube = new PowerTube(output);
            timer = new Timer();
            door = new Door();
            pB = new Button();
            tB = new Button();
            scB = new Button();
            userInterface = new UserInterface(pB, tB, scB, door, display, light, cookController);
            cookController = new CookController(timer, display, powerTube, userInterface);
        }

        [Test]
        public void OnPowerPressed_PressedOnce_CorrectOutput()
        {
            str = new StringWriter();
            Console.SetOut(str);
            pB.Press();
            StringAssert.Contains($"Display shows: 50 W", str.ToString());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        //[TestCase(10)]
        //[TestCase(15)]
        public void OnPowerPressed_PressedSeveral_CorrectOutput(int value)
        {
            str = new StringWriter();
            Console.SetOut(str);
            for (int i = 0; i < value; ++i)
            {
                pB.Press();
            }
            value = value * 50; // powerlevel er altid 50
            StringAssert.Contains($"Display shows: {value} W", str.ToString());
        }

        //[TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void OnStartPressed_PressedSeveral_CorrectOutput(int value)
        {
            str = new StringWriter();
            Console.SetOut(str);
            pB.Press();
            for (int i = 0; i < value; ++i)
            {
                tB.Press();
            }
            --value;
            StringAssert.Contains($"Display shows: {value}:", str.ToString());
        }




    }
}