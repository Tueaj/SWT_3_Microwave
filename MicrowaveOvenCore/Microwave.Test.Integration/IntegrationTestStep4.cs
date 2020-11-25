using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep4
    {
        private StringWriter str;
        private IOutput output;
        private IDisplay displayUT;
        private IPowerTube powerTubeUT;
        private ITimer TimerUT;
        private ICookController CookControllerUT;
        private IUserInterface userinterfaceSUB;
        private IButton ButtonSUB;
        private IDoor DoorSUB;


        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            output = new Output();
            displayUT = new Display(output);
            powerTubeUT = new PowerTube(output);
            TimerUT = new Timer();

            userinterfaceSUB = Substitute.For<IUserInterface>(default);
            CookControllerUT = new CookController(TimerUT,displayUT,powerTubeUT,userinterfaceSUB);
        }

        [TestCase(1, 40)]
        [TestCase(100, 10)]
        public void CookController_StartCookingCalled_PowertubeStarts(int power, int time)
        {
            CookControllerUT.StartCooking(power, time);

            Assert.That(str.ToString().Contains($"PowerTube works with {power}"));
        }

        [Test]
        public void CookController_stopCalled_PowertubeStops()
        {
            CookControllerUT.StartCooking(50,10);

            CookControllerUT.Stop();
            Assert.That(str.ToString().Contains($"PowerTube turned off"));
        }

    }
}