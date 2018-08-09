using NHentaiSharp.Core;
using System.Threading.Tasks;
using Xunit;

namespace NHentaiSharp.UnitTests
{
    public class Program
    {
        [Fact]
        public async Task BasicTest()
        {
            await new SearchClient()
                .WithTags("school swimsuit", "loli", "full color")
                .WithPage(2)
                .ExecuteAsync();
        }
    }
}
