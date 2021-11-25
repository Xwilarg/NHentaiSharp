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
                (await SearchClient.SearchByTagIdAsync(29565, 1)).elements[0].tags.Select(x => x.id));
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
            Assert.Equal("https://t.nhentai.net/galleries/923606/cover.png", res.cover.imageUrl.AbsoluteUri);
            Assert.Equal("https://t.nhentai.net/galleries/923606/thumb.png", res.thumbnail.imageUrl.AbsoluteUri);
            Assert.Equal("https://i.nhentai.net/galleries/923606/1.png", res.pages[0].imageUrl.AbsoluteUri);
            Assert.Equal("https://t.nhentai.net/galleries/923606/1t.png", res.pages[0].previewUrl.AbsoluteUri);
            Assert.Equal("https://nhentai.net/g/161194", res.url.AbsoluteUri);
        }

        [Fact]
        public async Task SearchByTags()
        {
            string[] tags = new string[] { "lolicon ", SearchClient.GetExactTag("full color"),
                SearchClient.GetCategoryTag("kantai", TagType.Parody),
                SearchClient.GetExcludeTag("drugs"), SearchClient.GetExcludeTag("rape") };
            var res = (await SearchClient.SearchWithTagsAsync(tags, 1)).elements[0];
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
                await SearchClient.SearchWithTagsAsync("");
            });
        }

        [Fact]
        public async Task SearchWithInvalidTags()
        {
            await Assert.ThrowsAsync<InvalidArgumentException>(async delegate ()
            {
                await SearchClient.SearchWithTagsAsync("aaaaaaaaaaaaaaaa");
            });
        }

        [Fact]
        public async Task SearchWithInvalidPage()
        {
            var result = await SearchClient.SearchByIdAsync(155974);
            Assert.Equal(PageFormat.INVALID, result.pages[1].format);
        }

        [Fact]
        public async Task Search()
        {
            Assert.NotEmpty((await SearchClient.SearchAsync()).elements);
        }

        [Fact]
        public async Task SearchWithInvalidId()
        {
            await Assert.ThrowsAsync<InvalidArgumentException>(async delegate ()
            {
                await SearchClient.SearchByIdAsync(2000000);
            });
        }

        [Fact]
        public async Task SearchByTagsWithSort()
        {
            var res = (await SearchClient.SearchWithTagsAsync(new[] { "lolicon" }, 1)).elements[0];
            var res2 = (await SearchClient.SearchWithTagsAsync(new[] { "lolicon" }, 1, PopularitySort.AllTime)).elements[0];
            Assert.NotEqual(res.id, res2.id);
        }
    }
}
