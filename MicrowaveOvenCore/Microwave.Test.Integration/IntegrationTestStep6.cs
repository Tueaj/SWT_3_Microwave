using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestStep6
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;

        private IDoor _doorUT;
        private IUserInterface _userInterfaceUT;

        [SetUp]
        public void Setup()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();

            _doorUT = new Door();
            _userInterfaceUT = new UserInterface(_powerButton, _timeButton, _startCancelButton, 
                _doorUT, _display, _light, _cookController);
        }

        #region OnDoorOpenedEvent
        [Test]
        public void Userinterface_OnDoorOpenedEventAtREADYState_LightRecivedTurnOnCall()
        {
            //Arrange
            //State start at READY State

            //Act
            _doorUT.Open();

            //Assert
            _light.Received().TurnOn();
        }

        [Test]
        public void Userinterface_OnDoorOpenedEventAtSETPOWERState_LightRecivedTurnOnCall()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButton.Pressed += Raise.Event();

            //Act
            _doorUT.Open();

            //Assert
            _light.Received().TurnOn();
        }

        [Test]
        public void Userinterface_OnDoorOpenedEventAtSETPOWERState_DisplayRecivedClearCall()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButton.Pressed += Raise.Event();

            //Act
            _doorUT.Open();

            //Assert
            _display.Received().Clear();
        }

        [Test]
        public void Userinterface_OnDoorOpenedEventAtSETTIMEState_LightRecivedTurnOnCall()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButton.Pressed += Raise.Event();
            //Setting state to SETTIME
            _timeButton.Pressed += Raise.Event();

            //Act
            _doorUT.Open();

            //Assert
            _light.Received().TurnOn();
        }

        [Test]
        public void Userinterface_OnDoorOpenedEventAtSETTIMEState_DisplayRecivedClearCall()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButton.Pressed += Raise.Event();
            //Setting state to SETTIME
            _timeButton.Pressed += Raise.Event();

            //Act
            _doorUT.Open();

            //Assert
            _display.Received().Clear();
        }


        [Test]
        public void Userinterface_OnDoorOpenedEventAtCOOKINGState_CookerRecivedStopCall()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButton.Pressed += Raise.Event();
            //Setting state to SETTIME
            _timeButton.Pressed += Raise.Event();
            //Setting state to COOKING
            _startCancelButton.Pressed += Raise.Event();

            //Act
            _doorUT.Open();

            //Assert
            _cookController.Received().Stop();
        }

        [Test]
        public void Userinterface_OnDoorOpenedEventAtCOOKINGState_DisplayRecivedClearCall()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButton.Pressed += Raise.Event();
            //Setting state to SETTIME
            _timeButton.Pressed += Raise.Event();
            //Setting state to COOKING
            _startCancelButton.Pressed += Raise.Event();

            //Act
            _doorUT.Open();

            //Assert
            _display.Received().Clear();
        }

        #endregion


        #region OnDoorOpenedEvent
        [Test]
        public void Userinterface_OnDoorClosedEventAtDOOROPENState_LightRecivedTurnOffCall()
        {
            //Arrange
            //Setting state to DOOROPEN
            _doorUT.Open();

            //Act
            _doorUT.Close();

            //Assert
            _light.Received().TurnOff();
        }

        #endregion





    }
}