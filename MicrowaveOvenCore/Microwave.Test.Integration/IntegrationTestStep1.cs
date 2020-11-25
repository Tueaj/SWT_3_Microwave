using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep1
    {
        private StringWriter str;
        private IOutput _outputUT;
        private ILight _lightUT;

            [SetUp]
            public void Setup()
            {
                str = new StringWriter();
                Console.SetOut(str);
                _outputUT = new Output();
                _lightUT = new Light(_outputUT);
            }

            

            [Test]
            public void Output_TurnOnCalledOnLight_LightIsTurnedOnWrittenOnConsole()
            {
                //Arrange

                //Act
                _lightUT.TurnOn();
                
                //Assert
                Assert.That(str.ToString().Contains("Light is turned on"));

            }

            [Test]
            public void Output_TurnOffCalledOnLight_LightIsTurnedOffWrittenOnConsole()
            {
                //Arrange
                _lightUT.TurnOn();
                str.Flush();

                //Act
                _lightUT.TurnOff();

                //Assert
                Assert.That(str.ToString().Contains("Light is turned off"));

            }

    }
}