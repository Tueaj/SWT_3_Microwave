using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
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

        [TestCase(50)]
        [TestCase(1)]
        [TestCase(100)]
        public void Output_TurnOnCalledWith50PowerOnPowerTube_ShowsPowerTubeWorksWithPowerOnConsole(int p)
        {
            //Arrange

            //Act
            _powerTubeUT.TurnOn(p);

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube works with {p}"));

        }

        [TestCase(0)]
        [TestCase(101)]
        public void Output_TurnOnCalledWith0PowerOnPowerTube_ArgumentOutOfRangeExceptionThrown(int p)
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(delegate { _powerTubeUT.TurnOn(p); });

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

        [Test]
        public void Output_TurnOnCalledWhilePowerTubeOn_ApplicationExceptionThrown()
        {
            //Arrange
            int power = 50;
            _powerTubeUT.TurnOn(power);
            str.Flush();

            //Act

            //Assert
            Assert.Throws<ApplicationException>(delegate { _powerTubeUT.TurnOn(power);});

        }

    }
}