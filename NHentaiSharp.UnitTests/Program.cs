using NHentaiSharp.Core;
using Xunit;

namespace NHentaiSharp.UnitTests
{
    public class Program
    {
        [Fact]
        public void BasicTest()
        {
            new SearchClient()
                .WithTags("school swimsuit")
                .WithPage(2);
        }
    }
}
