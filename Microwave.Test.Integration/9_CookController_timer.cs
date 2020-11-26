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
        public void 

    }
}