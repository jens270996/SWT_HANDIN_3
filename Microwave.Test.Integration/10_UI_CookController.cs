using System;
using System.Diagnostics;
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
        private CookController cookController;
        private IPowerTube powerTube;
        private IOutput output;
        private IDisplay display;
        private UserInterface userInterface;
        private IButton pB;
        private IButton tB;
        private IButton scB;
        private IDoor door;
        private ILight light;
        private StringWriter str;

        [SetUp]
        public void Setup()
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

            var cooker = new CookController(timer, display, powerTube);
            userInterface = new UserInterface(pB, tB, scB, door, display, light, cookController);
            cooker.UI = userInterface;
            cookController = cooker;
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

        
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void OnPowerPressedSeveral_StartPressed_StartsCooking(int value)
        {
            str = new StringWriter();
            Console.SetOut(str);
            
            
            for (int i = 0; i < value; ++i)
            {
                pB.Press();
            }
            tB.Press();
            scB.Press();
            StringAssert.Contains($"PowerTube works with {value*50/7}", str.ToString());
        }
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void OnTimerPressedSeveral_StartPressed_CorrectTime(int value)
        {
            str = new StringWriter();
            Console.SetOut(str);


                pB.Press();

            for (int i = 0; i < value; ++i)
            {
                tB.Press();
            }

            var watch = new Stopwatch();
            timer.Expired += (object o, EventArgs e) => watch.Stop();
            watch.Start();
            scB.Press();
            
            

            //str.Received().WriteLine($"Display cleared")
            
            while (watch.IsRunning) {
                if (watch.ElapsedMilliseconds > 60000) break;
            }
            Assert.AreEqual(value * 60 * 1000, watch.ElapsedMilliseconds, 1000);
        }
    }
}