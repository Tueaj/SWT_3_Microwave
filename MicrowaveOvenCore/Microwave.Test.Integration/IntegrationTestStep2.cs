using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep2
    {
        private StringWriter str;
        private IOutput _outputUT;
        private IDisplay _displayUT;

        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            _outputUT = new Output();
            _displayUT = new Display(_outputUT);
        }
        
        [Test]
        public void Output_ShowTimeCalledOnDisplay_DisplayShowsGivenTimeOnConsole()
        {
            //Arrange
            int min = 3;
            int sec = 55;

            //Act
            _displayUT.ShowTime(min,sec);

            //Assert
            Assert.That(str.ToString().Contains($"Display shows: {min:D2}:{sec:D2}"));

        }

        [Test]
        public void Output_ShowPowerCalledOnDisplay_DisplayShowsGivenPowerOnConsole()
        {
            //Arrange
            int power = 500;

            //Act
            _displayUT.ShowPower(power);

            //Assert
            Assert.That(str.ToString().Contains($"Display shows: {power} W"));

        }

        [Test]
        public void Output_ClearCalledOnDisplay_DisplayShowsDisplayClearedConsole()
        {
            //Arrange

            //Act
            _displayUT.Clear();

            //Assert
            Assert.That(str.ToString().Contains($"Display cleared"));

        }

    }
    
}