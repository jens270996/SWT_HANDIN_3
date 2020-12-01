using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    public class TimerDriver : ITimer
    {
        public int TimeRemaining => throw new NotImplementedException();

        public event EventHandler Expired;
        public event EventHandler TimerTick;

        public void Start(int time)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void Tick()
        {
            TimerTick?.Invoke(this, EventArgs.Empty);
        }
        public void Expire()
        {
            Expired?.Invoke(this, System.EventArgs.Empty);
        }
    }
    public class _8_CookControl_PowerTube_Disp_Output
    {
        private CookController cooker;
        private IUserInterface ui;
        private ITimer timer;
        private IPowerTube powerTube;
        private IDisplay display;
        private IOutput output;

        private StringWriter str;
        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer =Substitute.For<ITimer>();
            output = new Output();
            display = new Display(output);
            powerTube = new PowerTube(output);

            cooker = new CookController(timer, display, powerTube);
            cooker.UI = ui;

            str = new StringWriter();
            Console.SetOut(str);
        }

        [Test]
        public void TickEventInvoked_CookerReacts()
        {
            cooker.StartCooking(80, 60);
            timer.TimeRemaining.Returns(59);
            timer.TimerTick += Raise.EventWith(this, System.EventArgs.Empty);
            //StringAssert.Contains($"Display shows: {0}:{59}", str.ToString());
            StringAssert.Contains($"Display shows: {0:d2}:{59:d2}", str.ToString());
        }
        [Test]
        public void ExpireEventInvoked_CookingIsDoneCalled()
        {
            cooker.StartCooking(80, 60);
            timer.TimeRemaining.Returns(59);
            timer.TimerTick += Raise.EventWith(this, System.EventArgs.Empty);
            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received(1).CookingIsDone();
            
        }

        [Test]
        public void ExpireEventInvoked_TubeTurnedOff()
        {
            cooker.StartCooking(80, 60);
            timer.TimeRemaining.Returns(59);
            timer.TimerTick += Raise.EventWith(this, System.EventArgs.Empty);
            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            
            StringAssert.Contains($"PowerTube turned off", str.ToString());
        }

    }
}
