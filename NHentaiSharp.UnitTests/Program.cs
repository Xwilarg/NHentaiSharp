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
        public async Task SearchWithTagId()
        {
            Assert.Contains(29565,
                (await new SearchClient()
                .WithMax(1)
                .ExecuteWithTagIdAsync(29565)).elements[0].tags.Select(x => x.id));
        }

        [Fact]
        public async Task SearchWithId()
        {
            var res = (await new SearchClient()
                .WithMax(1)
                .ExecuteWithIdAsync(161194)).elements[0];
            Assert.Equal("[ユイザキカズヤ] つなかん。 (COMIC ポプリクラブ 2013年8月号) [英訳]", res.japaneseTitle);
            Assert.Equal("Tsuna-kan. | Tuna Can", res.prettyTitle);
            Assert.Equal("[Yuizaki Kazuya] Tsuna-kan. | Tuna Can (COMIC Potpourri Club 2013-08) [English] [PSYN]", res.englishTitle);
            Assert.Equal("180413", res.uploadDate.ToString("yyMMdd"));
            Assert.Contains(19440, res.tags.Select(x => x.id));
            Assert.Equal(17, res.numPages);
            Assert.Equal(17, res.pages.Length);
        }

        [Fact]
        public async Task SearchWithTags()
        {
            string tags = "lolicon " + SearchClient.GetExactTag("full color") + " " +
                SearchClient.GetCategoryTag("kantai", TagType.Parody) + " " +
                SearchClient.GetExcludeTag("drugs") + " " + SearchClient.GetExcludeTag("rape");
            var res = (await new SearchClient()
                  .WithMax(1)
                  .ExecuteWithTagsAsync(tags)).elements[0];
            var ids = res.tags.Select(x => x.id);
            Assert.Contains(1841, ids);
            Assert.Contains(19440, ids);
            Assert.DoesNotContain(22079, ids);
            Assert.DoesNotContain(27553, ids);
        }

        [Fact]
        public async Task SearchWithEmptyTags()
        {
            await Assert.ThrowsAsync<EmptySearchException>(async delegate ()
            {
                await new SearchClient()
                    .WithMax(1)
                    .ExecuteWithTagsAsync();
            });
        }

        [Fact]
        public async Task Search()
        {

            Assert.NotEmpty((await new SearchClient()
                .WithMax(1)
                .ExecuteAsync()).elements);
        }
    }
}
