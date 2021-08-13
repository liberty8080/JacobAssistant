using JacobAssistant.Bots.Commands;
using NUnit.Framework;

namespace JacobAssistant.Tests.Bots
{
    [TestFixture]
    public class CommandParseTest
    {
        [TestCase("help",ExpectedResult = true)]
        [TestCase("/help",ExpectedResult=false)]
        [TestCase("/",ExpectedResult = false)]
        [TestCase("11",ExpectedResult = false)]
        [TestCase("蛤蛤",ExpectedResult = false)]
        // [TestCase()]
        public bool IsCommandTest(string value)
        {
           return ICommand.HasCommand(value);
        }

        [TestCase("help",ExpectedResult = null)]
        [TestCase("/help",ExpectedResult="help")]
        [TestCase("/",ExpectedResult = null)]
        [TestCase("11",ExpectedResult = null)]
        [TestCase("蛤蛤",ExpectedResult = null)]
        [TestCase("/help 111",ExpectedResult = "help")]
        public string TryParseCommandTest(string value)
        {
            ICommand.TryParseCommandName(value,out var commandName);
            return commandName;
        }
    }
}