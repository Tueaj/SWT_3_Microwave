using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep3
    {
        private StringWriter str;
        private IOutput _outputUT;
        private IPowerTube _powerTubeUT;

        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            _outputUT = new Output();
            _powerTubeUT = new PowerTube(_outputUT);
        }

        [Test]
        public void Output_TurnOnCalledWith50PowerOnPowerTube_ShowsPowerTubeWorksWithPowerOnConsole()
        {
            //Arrange
            int power = 50;

            //Act
            _powerTubeUT.TurnOn(power);

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube works with {power}"));

        }

        [Test]
        public void Output_TurnOnCalledWith0PowerOnPowerTube_ArgumentOutOfRangeExceptionThrown()
        {
            //Arrange
            int power = 0;

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(delegate { _powerTubeUT.TurnOn(power); });

        }

        [Test]
        public void Output_TurnOnCalledWith1PowerOnPowerTube_ShowsPowerTubeWorksWithPowerOnConsole()
        {
            //Arrange
            int power = 1;

            //Act
            _powerTubeUT.TurnOn(power);

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube works with {power}"));

        }
        [Test]
        public void Output_TurnOnCalledWith100PowerOnPowerTube_ShowsPowerTubeWorksWithPowerOnConsole()
        {
            //Arrange
            int power = 100;

            //Act
            _powerTubeUT.TurnOn(power);

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube works with {power}"));

        }

        [Test]
        public void Output_TurnOnCalledWith101PowerOnPowerTube_ArgumentOutOfRangeExceptionThrown()
        {
            //Arrange
            int power = 101;

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(delegate { _powerTubeUT.TurnOn(power); });

        }

        [Test]
        public void Output_TurnOffCalledOnPowerTube_ShowsPowerTubeTurnedOffOnConsole()
        {
            //Arrange
            int power = 50;
            _powerTubeUT.TurnOn(power);
            str.Flush();

            //Act
            _powerTubeUT.TurnOff();

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube turned off"));

        }

    }
}