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

    
    public class IntegrationTestStep5
    {
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IUserInterface _uInterface;
        private IDoor door;
        private ILight light;
        private ICookController cookController;
        private IDisplay display;

        [SetUp]
        public void Setup()
        {
            cookController = Substitute.For<ICookController>();
            door = Substitute.For<IDoor>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            _uInterface = new UserInterface(
                powerButton,
                timeButton,
                startCancelButton,
                door,
                display,
                light,
                cookController);
        }




        [Test]
        public void interfaceREADYstate_powerButtonPressed_displayShowPowerMethodCalled()
        {
            //Arrange

            //Act
            powerButton.Press();


            //Assert
            display.ReceivedWithAnyArgs().ShowPower(Arg.Any<int>());


        }

        [Test]
        public void interfaceSETPOWERstate_powerButtonPressed_displayShowPowerMethodCalled()
        {
            //Arrange
            powerButton.Press(); //Assuring UserInterface is in SETPOWER state. 
            //Act

            powerButton.Press();

            //Assert
            display.ReceivedWithAnyArgs().ShowPower(Arg.Any<int>());


        }

        [Test]
        public void interfaceREADYstate_timeButtonPressed_displayShowTimeCalled()
        {
            //Arrange
            
            //Act

            timeButton.Press();

            //Assert

            display.DidNotReceiveWithAnyArgs().ShowTime(Arg.Any<int>(), Arg.Any<int>());


        }

       

        [Test]
        public void interfaceSETTIMEstate_timeButtonPressed_displayShowTimeCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
            timeButton.Press(); //Assuring UserInterface is in SETTIME state.
            //Act

            timeButton.Press();

            //Assert

            display.ReceivedWithAnyArgs().ShowTime(Arg.Any<int>(), Arg.Any<int>());


        }

        [Test]
        public void interfaceSetPowerstate_StartCancelButtonPressed_lightTurnOffCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
           
            //Act

            startCancelButton.Press();

            //Assert

            light.ReceivedWithAnyArgs().TurnOff();


        }

        [Test]
        public void interfaceSETPOWERstate_StartCancelButtonPressed_displayCLEARCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.

            //Act

            startCancelButton.Press();

            //Assert

            display.ReceivedWithAnyArgs().Clear();


        }
        [Test]
        public void interfacSETTIMEEstate_StartCancelButtonPressed_lightTurnOnCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
            timeButton.Press(); //Assuring UserInterface is in SETTIME state.

            //Act

            startCancelButton.Press();

            //Assert

            light.ReceivedWithAnyArgs().TurnOn();


        }

        [Test]
        public void interfacSETTIMEEstate_StartCancelButtonPressed_CookerStartCookingnCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
            timeButton.Press(); //Assuring UserInterface is in SETTIME state.

            //Act

            startCancelButton.Press();

            //Assert

            cookController.ReceivedWithAnyArgs().StartCooking(Arg.Any<int>(),Arg.Any<int>());


        }

        [Test]
        public void interfaceCOOKINGstate_StartCancelButtonPressed_CookerSTOPCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
            timeButton.Press(); //Assuring UserInterface is in SETTIME state.
            startCancelButton.Press(); //Assuring UserInterface is in COOKING state

            //Act

            startCancelButton.Press();

            //Assert

            cookController.Received().Stop();


        }

        [Test]
        public void interfaceCOOKINGstate_StartCancelButtonPressed_LightOFFCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
            timeButton.Press(); //Assuring UserInterface is in SETTIME state.
            startCancelButton.Press(); //Assuring UserInterface is in COOKING state

            //Act

            startCancelButton.Press();

            //Assert

            light.Received().TurnOff();


        }

        [Test]
        public void interfaceCOOKINGstate_StartCancelButtonPressed_DisplayCLEARCalled()
        {
            //Arrange

            powerButton.Press(); //Assuring UserInterface is in SETPOWER state.
            timeButton.Press(); //Assuring UserInterface is in SETTIME state.
            startCancelButton.Press(); //Assuring UserInterface is in COOKING state

            //Act

            startCancelButton.Press();

            //Assert

            display.Received().Clear();


        }


    }
}