using System;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class IntegrationTestStep7
    {
        private StringWriter str;
        private IOutput _outputUT;
        private ILight _lightUT;
        private IDisplay _displayUT;
        private IPowerTube _powerTubeUT;
        private ITimer _timerUt;
        private ICookController _cookControllerUT;
        private IButton _powerButtonUT;
        private IButton _timeButtonUT;
        private IButton _startCancelButtonUT;
        private IDoor _doorUT;
        private IUserInterface _userInterfaceUT;


        [SetUp]
        public void Setup()
        {
            str = new StringWriter();
            Console.SetOut(str);
            _outputUT = new Output();
            _lightUT = new Light(_outputUT);
            _displayUT = new Display(_outputUT);
            _powerTubeUT = new PowerTube(_outputUT);
            _timerUt = new Timer();
            _cookControllerUT = new CookController(_timerUt,_displayUT,_powerTubeUT);
            _powerButtonUT = new Button();
            _timeButtonUT = new Button();
            _startCancelButtonUT = new Button();
            _doorUT = new Door();
            _userInterfaceUT = new UserInterface(_powerButtonUT,_timeButtonUT,_startCancelButtonUT,_doorUT,_displayUT,_lightUT,_cookControllerUT);
        }

        #region ButtonPressing
        [Test]
        public void Output_powerButtonPressed_ShowsPowerOn50OnConsole()
        {
            //Arrange

            //Act
            _powerButtonUT.Press();


            //Assert
            Assert.That(str.ToString().Contains($"Display shows: 50 W"));
        }

        [TestCase(1,50)]
        [TestCase(2, 100)]
        [TestCase(3, 150)]
        [TestCase(4, 200)]
        [TestCase(5, 250)]
        [TestCase(6, 300)]
        [TestCase(7, 350)]
        [TestCase(8, 400)]
        [TestCase(9, 450)]
        [TestCase(10, 500)]
        [TestCase(11, 550)]
        [TestCase(12, 600)]
        [TestCase(13, 650)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void Output_powerButtonPressed_ShowsPowerWith50IntervalUntilResetCalled(int pressedamount, int power)
        {
            //Arrange
            _powerButtonUT.Press(); //Assuring UserInterface is in SETPOWER state. 

            //Act
            for (int i = 0; i < pressedamount; i++)
                _powerButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains($"Display shows: {power} W"));
        }

        [TestCase(1, "01")]
        [TestCase(2, "02")]
        [TestCase(3, "03")]
        [TestCase(4, "04")]
        [TestCase(5, "05")]
        [TestCase(6, "06")]
        [TestCase(7, "07")]
        [TestCase(8, "08")]
        [TestCase(9, "09")]
        [TestCase(10, "10")]
        public void Output_timeButtonPressed_ShowsTimeWith1MinIntervalCalled(int pressedamount, string time)
        {
            //Arrange
            _powerButtonUT.Press();
            StringBuilder sb = str.GetStringBuilder().Clear();

            //Act
            for (int i = 0; i < pressedamount; i++)
                _timeButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains($"Display shows: {time}:00"));
        }

        
        [Test]
        public void Output_StartCancelButtonPressedFromREADYState_ShowsDisplayCleared()
        {
            //Arrange
            _powerButtonUT.Press();
            StringBuilder sb = str.GetStringBuilder().Clear();

            //Act
            _startCancelButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains("Display cleared"));
        }

        [Test]
        public void Output_StartCancelButtonPressedFromSETTIMEState_ShowsLightInOn()
        {
            //Arrange
            _powerButtonUT.Press(); //Assuring UserInterface is in SETPOWER state.
            _timeButtonUT.Press(); //Assuring UserInterface is in SETTIME state.
            StringBuilder sb = str.GetStringBuilder().Clear();

            //Act
            _startCancelButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void Output_StartCancelButtonPressedFromSETTIMEState_ShowsPowertubeIsOn()
        {
            //Arrange
            _powerButtonUT.Press(); //Assuring UserInterface is in SETPOWER state.
            _timeButtonUT.Press(); //Assuring UserInterface is in SETTIME state.
            StringBuilder sb = str.GetStringBuilder().Clear();

            //Act
            _startCancelButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains($"PowerTube works with"));
        }

        [Test]
        public void Output_StartCancelButtonPressedFromCOOKINGState_ShowsPowertubeIsOff()
        {
            //Arrange
            _powerButtonUT.Press(); //Assuring UserInterface is in SETPOWER state.
            _timeButtonUT.Press(); //Assuring UserInterface is in SETTIME state.
            _startCancelButtonUT.Press();

            //Act
            _startCancelButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains("PowerTube turned off"));
        }

        [Test]
        public void Output_StartCancelButtonPressedFromCOOKINGState_ShowsLigtIsTurnedOff()
        {
            //Arrange
            _powerButtonUT.Press(); //Assuring UserInterface is in SETPOWER state.
            _timeButtonUT.Press(); //Assuring UserInterface is in SETTIME state.
            _startCancelButtonUT.Press();

            //Act
            _startCancelButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains("Light is turned off"));
        }
        [Test]
        public void Output_StartCancelButtonPressedFromCOOKINGState_ShowsDisplayCleared()
        {
            //Arrange
            _powerButtonUT.Press(); //Assuring UserInterface is in SETPOWER state.
            _timeButtonUT.Press(); //Assuring UserInterface is in SETTIME state.
            _startCancelButtonUT.Press();

            //Act
            _startCancelButtonUT.Press();

            //Assert
            Assert.That(str.ToString().Contains("Display cleared"));
        }
        #endregion

        #region DoorSimunlation

        [Test]
        public void Output_OnDoorOpenedEventAtREADYState_ShowsLightIsTurnedOn()
        {
            //Arrange
            //State start at READY State

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("Light is turned on"));
        }

        [Test]
        public void Output_OnDoorOpenedEventAtSETPOWERState_ShowsLightIsTurnedOn()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButtonUT.Press();

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("Light is turned on"));
        }
        
        [Test]
        public void Output_OnDoorOpenedEventAtSETPOWERState_ShowsDisplayIsCleared()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButtonUT.Press();

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("Display cleared"));
        }
        
        [Test]
        public void Output_OnDoorOpenedEventAtSETTIMEState_ShowsLightIsTurnedOn()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButtonUT.Press();
            //Setting state to SETTIME
            _timeButtonUT.Press();

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("Light is turned on"));
        }
        
        [Test]
        public void Output_OnDoorOpenedEventAtSETTIMEState_ShowsDisplayIsCleared()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButtonUT.Press();
            //Setting state to SETTIME
            _timeButtonUT.Press();

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("Display cleared"));
        }

        
        [Test]
        public void Output_OnDoorOpenedEventAtCOOKINGState_ShowsPowertubeIsOff()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButtonUT.Press();
            //Setting state to SETTIME
            _timeButtonUT.Press();
            //Setting state to COOKING
            _startCancelButtonUT.Press();

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("PowerTube turned off"));
        }
        
        [Test]
        public void Output_OnDoorOpenedEventAtCOOKINGState_ShowsDisplayIsCleared()
        {
            //Arrange
            //Setting state to SETPOWER
            _powerButtonUT.Press();
            //Setting state to SETTIME
            _timeButtonUT.Press();
            //Setting state to COOKING
            _startCancelButtonUT.Press();

            //Act
            _doorUT.Open();

            //Assert
            Assert.That(str.ToString().Contains("Display cleared"));
        }
        
        [Test]
        public void Output_OnDoorClosedEventAtDOOROPENState_LightRecivedTurnOffCall()
        {
            //Arrange
            //Setting state to DOOROPEN
            _doorUT.Open();

            //Act
            _doorUT.Close();

            //Assert
            Assert.That(str.ToString().Contains("Light is turned off"));
        }
        

    } 
        #endregion
    
    }