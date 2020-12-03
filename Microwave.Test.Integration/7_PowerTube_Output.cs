using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class PowerTube_Output
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

        [TestCase(50)]
        [TestCase(150)]
        [TestCase(100)]
        public void StartCooking_TurnOnCalledWithCorrectValues_OutputWithCorrectValues(int value)
        {
            str = new StringWriter();
            Console.SetOut(str);
            cookControllerDriver.StartCooking(value);
            StringAssert.Contains( $"PowerTube works with {Math.Ceiling((decimal)value/7)}", str.ToString());
        }

        [TestCase(0)]
        [TestCase(800)]
        [TestCase(-5)]
        public void StartCooking_TurnOnCalledWithWrongValues_ExceptionThrown(int value)
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cookControllerDriver.StartCooking(value));
        }

        [Test]
        public void StartCookingAlreadyOn_TurnOnCalled_ExceptionThrown()
        {
            cookControllerDriver.StartCooking(50);
            ApplicationException ex = Assert.Throws<ApplicationException>(() =>
                cookControllerDriver.StartCooking(50));
            Assert.That(ex.Message, Is.EqualTo("PowerTube.TurnOn: is already on"));
        }

        [Test]
        public void StopCalled()
        {
            str = new StringWriter();
            Console.SetOut(str);
            cookControllerDriver.StartCooking(50);
            cookControllerDriver.Stop();
            StringAssert.Contains($"PowerTube turned off", str.ToString());
        }
    }
}
