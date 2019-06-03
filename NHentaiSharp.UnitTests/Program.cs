using NHentaiSharp.Core;
using NHentaiSharp.Exception;
using NHentaiSharp.Search;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NHentaiSharp.UnitTests
{
    public class Program
    {
        [Fact]
        public async Task SearchByTagId()
        {
            Assert.Contains(29565,
                (await SearchClient.SearchByTagIdAsync(29565, 1, 1)).elements[0].tags.Select(x => x.id));
        }

        [Fact]
        public async Task SearchById()
        {
            var res = await SearchClient.SearchByIdAsync(161194);
            Assert.Equal("[ユイザキカズヤ] つなかん。 (COMIC ポプリクラブ 2013年8月号) [英訳]", res.japaneseTitle);
            Assert.Equal("Tsuna-kan. | Tuna Can", res.prettyTitle);
            Assert.Equal("[Yuizaki Kazuya] Tsuna-kan. | Tuna Can (COMIC Potpourri Club 2013-08) [English] [PSYN]", res.englishTitle);
            Assert.Equal("160413", res.uploadDate.ToString("yyMMdd"));
            Assert.Contains(19440, res.tags.Select(x => x.id));
            Assert.Equal(17, res.numPages);
            Assert.Equal(17, res.pages.Length);
            Assert.Equal(161194, res.id);
        }

        [Fact]
        public async Task SearchByTags()
        {
            string[] tags = new string[] { "lolicon ", SearchClient.GetExactTag("full color"),
                SearchClient.GetCategoryTag("kantai", TagType.Parody),
                SearchClient.GetExcludeTag("drugs"), SearchClient.GetExcludeTag("rape") };
            var res = (await SearchClient.SearchByTagsAsync(tags, 1, 1)).elements[0];
            var ids = res.tags.Select(x => x.id);
            Assert.Contains(1841, ids);
            Assert.Contains(19440, ids);
            Assert.DoesNotContain(22079, ids);
            Assert.DoesNotContain(27553, ids);
        }

        [Fact]
        public async Task SearchByEmptyTags()
        {
            await Assert.ThrowsAsync<EmptySearchException>(async delegate ()
            {
                await SearchClient.SearchByTagsAsync("");
            });
        }

        [Fact]
        public async Task Search()
        {

            Assert.NotEmpty((await SearchClient.SearchAsync()).elements);
        }
    }
}
