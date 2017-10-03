﻿using Codurance_Katacombs.Core;
using Codurance_Katacombs.Core.Controller;
using Codurance_Katacombs.Infrastructure;
using FakeItEasy;
using NUnit.Framework;

namespace Codurance_Katacombs.Tests.Core.Controller
{
    [TestFixture]
    public class KatacombsGameControllerShould
    {
        private IWrapConsole _console;
        private KatacombsController _katacombsController;
        private IKatacombsEngine _engine;

        [SetUp]
        public void TestSetup()
        {
            _engine = A.Fake<IKatacombsEngine>();
            _console = A.Fake<IWrapConsole>();
            _katacombsController = new KatacombsController(_engine, _console);
            _katacombsController.StartGame();
        }

        [Test]
        public void Process_user_input()
        {
            string commandText = "GO N";
            _console.ReadLine += Raise.FreeForm.With(commandText);

            A.CallTo(() => _engine.Execute(commandText)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Display_output()
        {
            var messageText = new []{ "CIAO MAMMA!", "BONAAAA"};
            _engine.DisplayMessage += Raise.FreeForm.With(new []{messageText});

            A.CallTo(() => _console.Write(messageText)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Startup_the_world()
        {
            A.CallTo(() => _engine.Startup()).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
