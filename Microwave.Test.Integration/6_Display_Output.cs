using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class _6_Display_Output
    {
        private IOutput output;
        private IDisplay display;

        private StringWriter str;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            display=new Display(output);

            str = new StringWriter();
            Console.SetOut(str);
        }

        [Test]
        public void ShowPower_PowerLogged()
        {
            int power = 1000;
            display.ShowPower(power);
            StringAssert.Contains($"Display shows: {power} W", str.ToString());

        }

        [Test]
        public void ShowTime_TimeLogged()
        {
            int min = 100;
            int sec = 80;
            display.ShowTime(min,sec);
            StringAssert.Contains($"Display shows: {min:D2}:{sec:D2}", str.ToString());
            
        }
        [Test]
        public void Clear_ClearLogged()
        {
            display.Clear();
            StringAssert.Contains($"Display cleared", str.ToString());
            
        }
    }
}
