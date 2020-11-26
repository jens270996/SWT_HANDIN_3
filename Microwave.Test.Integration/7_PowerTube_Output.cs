using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class PowerTube_Output
    {
   
        private IOutput output;
        private IPowerTube powerTube;
        private CookControllerDriver cookControllerDriver;
        private StringWriter str;
        

        [SetUp]
        public void Setup()
        {
            output = new Output();
            powerTube = new PowerTube(output);
            cookControllerDriver = new CookControllerDriver(powerTube);
        }

        [TestCase(1)]
        [TestCase(37)]
        [TestCase(100)]
        public void StartCooking_TurnOnCalled(int value)
        {
            str = new StringWriter();
            Console.SetOut(str);
            cookControllerDriver.StartCooking(value);
            StringAssert.Contains( $"PowerTube works with {value}", str.ToString());
        }
    }
}
