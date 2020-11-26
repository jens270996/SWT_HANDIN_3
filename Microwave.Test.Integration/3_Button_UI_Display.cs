using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class _3_Button_UI_Display
    {

        IButton pB;
        IButton tB;
        IButton scB;
        IDisplay disp;
        IOutput output;
        ICookController cooker;
        UserInterface ui;
        [SetUp]
        public void Setup()
        {
            var door = new Door();
            pB =new Button();
            tB = new Button();
            scB = new Button();
            output = Substitute.For<IOutput>();
            disp = new Display(output);

            cooker = Substitute.For<ICookController>();
            var light = Substitute.For<ILight>();


            ui = new UserInterface(pB, tB, scB, door, disp, light, cooker);
        }

        [Test]
        public void PowerButtonPressed_PowerShowed()
        {
            pB.Press();

            output.Received(1).OutputLine($"Display shows: 50 W");
            
        }


        [Test]
        public void Power_TimerButtonPressedTwice_TimeShowed()
        {

            pB.Press();
            tB.Press();
            tB.Press();
            int min = 1;
            int sec = 0;
            output.Received(1).OutputLine($"Display shows: {min:D2}:{sec:D2}");

        }

        [Test]
        public void Power_Timer_StartButtonPressed_TimeShowedOnTimerTick()
        {

            pB.Press();
            tB.Press();
            tB.Press();
            scB.Press();
            
            int min = 1;
            int sec = 0;
            output.Received(1).OutputLine($"Display shows: {min:D2}:{sec:D2}");

        }

    }
}
