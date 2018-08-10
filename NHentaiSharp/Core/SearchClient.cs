using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NHentaiSharp.Core
{
    public class SearchClient
    {
        public SearchClient(params string[] tags)
        {
            page = 1;
        }

        public SearchClient WithPage(int page)
        {
            this.page = page;
            return (this);
        }

        public SearchClient WithMax(int? max)
        {
            if (max < 0)
                throw new ArgumentException("max must be a positive value or be set to null.");
            this.max = max;
            return (this);
        }

        public static string GetExactTag(string tag)
            => "\"" + tag + "\"";

        public static string GetCategoryTag(string tag, Search.TagType type)
            => type.ToString().ToLower().Replace("y", "ie") + "s:" + tag;

        public static string GetExcludeTag(string tag)
            => "-" + tag;

        public async Task<Search.SearchResult> ExecuteWithTagsAsync(params string[] tags)
        {
            string allTags = string.Join(" ", tags);
            if (string.IsNullOrEmpty(allTags))
                throw new Exception.EmptySearchException();
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await(await hc.GetAsync("https://nhentai.net/api/galleries/search?query=" + allTags + "&page=" + page)).Content.ReadAsStringAsync()), max));
        }

        public async Task<Search.SearchResult> ExecuteWithTagIdAsync(int id)
        {
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/tagged?tag_id=" + id + "&page=" + page)).Content.ReadAsStringAsync()), max));
        }

        public async Task<Search.GalleryElement> ExecuteWithIdAsync(int id)
        {
            using (HttpClient hc = new HttpClient())
                return (new Search.GalleryElement(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/gallery/" + id)).Content.ReadAsStringAsync())));
        }

        public async Task<Search.SearchResult> ExecuteAsync()
        {
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/all")).Content.ReadAsStringAsync()), max));
        }

        private int page;
        private int? max;
    }
}
