using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NSubstitute;
using NUnit.Framework;


namespace Microwave.Test.Integration
{
    [TestFixture]
    public class CookController_timer
    {
        private ITimer timer;
        private ICookController cookController;
        private IPowerTube powerTube;
        private IOutput output;
        private IDisplay display;
        private IUserInterface userInterface;
        private StringWriter str;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            display = new Display(output);
            timer = new Timer();
            powerTube = new PowerTube(output);
            userInterface = Substitute.For<IUserInterface>();
            cookController = new CookController(timer, display, powerTube, userInterface);
        }

        [Test]
        public void Start_StartCalled_TimeRemainingIsPositive()
        {
            cookController.StartCooking(50, 10);
            Assert.That(timer.TimeRemaining > 1);// dårlig parameter at teste på
        }

        [Test]
        public void Stop_StopCalled_TimeRemainingIs0()
        {
            cookController.Stop();
            Assert.That(timer.TimeRemaining == 0);// dårlig parameter at teste på
        }


        [Test]
        public void OnTimerTick_RemainingTimeOutput_OutputSucces()
        {
            str = new StringWriter();
            Console.SetOut(str);
            cookController.StartCooking(50, 20);
            System.Threading.Thread.Sleep(1500);            // ikke optimalt
            StringAssert.Contains($"Display shows: ", str.ToString());
        }


        [Test]
        public void OnTimerExpiredEvent_EventOccured_CookingIsDoneCalled()
        {
            cookController.StartCooking(50, 1);
            System.Threading.Thread.Sleep(1500); // ikke optimalt
            userInterface.Received(1).CookingIsDone();   
        }

        [Test]
        public void OnTimerTickEvent_EventOccured_RemainingTimeDisplayed()
        {
            str = new StringWriter();
            Console.SetOut(str);
            cookController.StartCooking(50, 20);
            System.Threading.Thread.Sleep(1500); // ikke optimalt
            StringAssert.Contains($"Display shows: ", str.ToString());
        }

    }
}