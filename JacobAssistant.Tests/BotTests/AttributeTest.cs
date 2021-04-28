using System;
using JacobAssistant.Bot;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.BotTests
{
    public class AttributeTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public AttributeTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestTextCommand()
        {
            var type = typeof(Foo);
            foreach (var customAttribute in type.GetCustomAttributes(false))
            {
                Assert.Equal(typeof(TextCommandAttribute), customAttribute.GetType());
            }

            foreach (var i in type.GetMethods())
            {
                foreach (var o in i.GetCustomAttributes(false))
                {
                    if (o is not Cmd cmd) continue;
                    _testOutputHelper.WriteLine($"name:{cmd.Name}");
                    _testOutputHelper.WriteLine($"permission:{cmd.Permission}");
                    Assert.Equal("name", cmd.Name);
                    Assert.Equal(BotPermission.AnyOne, cmd.Permission);
                }
            }
        }
    }

    [TextCommand]
    public class Foo
    {
        public bool Invoked { get; set; }

        [Cmd("bar", Permission = BotPermission.AnyOne)]
        public void Bar()
        {
            Invoked = true;
            Console.WriteLine("1");
        }
    }
}