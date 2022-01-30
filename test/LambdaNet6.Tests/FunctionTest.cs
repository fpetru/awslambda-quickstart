using Xunit;
using Amazon.Lambda.TestUtilities;

namespace LambdaNet6.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var context = new TestLambdaContext();
            var upperCase = Function.FunctionHandler("hello world", context);

            Assert.Equal("Architecture: X64, .NET Version: 6.0.0 -- HELLO WORLD", upperCase);
        }
    }
}
