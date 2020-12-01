using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Test.Integration
{
    public class _2_Button_UI
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
            door = Substitute.For<IDoor>();
            pB = new Button();
            tB = new Button();
            scB = new Button();
            var disp = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();


            UserInterface ui = new UserInterface(pB, tB, scB, door, disp, light, cooker);
        }

        [TestCase(7,5)]

        [TestCase(1, 1)]
        public void Power_Timer_StartButton_CookerStarted(int a,int b)
        {
            for (int i = 0; i < a; i++)
            {
                pB.Press();
            }

            for (int i = 0; i < b; i++)
            {
                tB.Press();
                    }
            scB.Press();
            door.Open();
            cooker.Received(1).StartCooking(a*50,(b)*60);

        }


    }
}
