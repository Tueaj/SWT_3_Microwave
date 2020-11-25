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
        private IButton _buttonOne;
        private IButton _buttonTwo;
        private IButton _buttonThree;

        private IDoor _doorUT;
        private IUserInterface _userInterfaceUT;

        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;

        [SetUp]
        public void Setup()
        {
            _buttonOne = Substitute.For<IButton>();
            _buttonTwo = Substitute.For<IButton>();
            _buttonThree = Substitute.For<IButton>();

            _doorUT = new Door();
            _userInterfaceUT = new UserInterface(_buttonOne, _buttonTwo, _buttonThree, 
                _doorUT, _display, _light, _cookController);
        }

        


    }
}