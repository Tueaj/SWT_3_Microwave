using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

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

        [TestCase(50, 40)]
        [TestCase(1, 40)]
        [TestCase(100, 40)]
        public void CookController_StartCookingCalled_PowertubeStarts(int power, int time)
        {
            //Arrange

            //Act
            CookControllerUT.StartCooking(power, time);

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube works with {power}"));
        }

        [TestCase(0, 40)]
        [TestCase(101, 40)]
        public void CookController_StartCookingCalled_InvalidPowertubeValues(int power, int time)
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(delegate { CookControllerUT.StartCooking(power,time); });

        }

        [Test]
        public void CookController_StartCooking_WhileCookControllerIsAlreadyCooking()
        {
            //Arrange
            int power = 50;
            int time = 40;
            CookControllerUT.StartCooking(power, time);
            //Act

            //Assert
            Assert.Throws<ApplicationException>(delegate { CookControllerUT.StartCooking(power, time); });
        }

        [Test]
        public void CookController_stopCalled_PowertubeStops()
        {
            //Arrange
            CookControllerUT.StartCooking(50,10);
            //Act
            CookControllerUT.Stop();
            //Assert
            Assert.That(str.ToString().Contains($"PowerTube turned off"));
        }

        [TestCase(50,2)]
        [TestCase(50, 5)]
        [TestCase(50, 10)]
        [TestCase(50, 62)]
        public void CookController_StartCooking_TimerSendsTick(int power, int time)
        {
            //Arrange
            AutoResetEvent pause = new AutoResetEvent(false);
            StringWriter srExpect = new StringWriter();

            TimerUT.Expired += delegate(object sender, EventArgs e) { pause.Set(); };

            //Act
            CookControllerUT.StartCooking(power, time);
            int ticks = time;

            //Assert
            for (int i = 0; i < ticks; i++)
            {
                srExpect.WriteLine($"Display shows: {(time-1) / 60:D2}:{(time-1) % 60:D2}");
                time--;
            }

            pause.WaitOne(ticks * 1000 + 500);

            Assert.That(str.ToString().Contains(srExpect.ToString()));
            //Assert.That(srExpect.ToString(), Is.EqualTo(str.ToString()));
        }
    }
}