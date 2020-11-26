using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class _5_Light_Output
    {
        private IOutput output;
        private ILight light;
        
        private StringWriter str;


        [SetUp]
        public void Setup()
        {
            output = new Output();
            light = new Light(output);
            
            str = new StringWriter();
            Console.SetOut(str);
        }


        [Test]
        public void LightOn_LoggedLightOn()
        {
            light.TurnOn();
            StringAssert.Contains("Light is turned on", str.ToString());
            
        }

        [Test]
        public void LightOnOff_LoggedLightOff()
        {
            light.TurnOn();
            light.TurnOff();
            StringAssert.Contains("Light is turned off", str.ToString());

        }
    }
}
