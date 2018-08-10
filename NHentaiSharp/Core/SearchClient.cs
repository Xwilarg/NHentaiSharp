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
                return (ParseSearchResult(JsonConvert.DeserializeObject(await(await hc.GetAsync("https://nhentai.net/api/galleries/search?query=" + allTags + "&page=" + page)).Content.ReadAsStringAsync())));
        }

        public async Task<Search.SearchResult> ExecuteWithTagIdAsync(int id)
        {
            using (HttpClient hc = new HttpClient())
                return (ParseSearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/tagged?tag_id=" + id + "&page=" + page)).Content.ReadAsStringAsync())));
        }

        public async Task<Search.SearchResult> ExecuteWithIdAsync(int id)
        {
            using (HttpClient hc = new HttpClient())
                return (ParseSearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/" + id)).Content.ReadAsStringAsync())));
        }

        public async Task<Search.SearchResult> ExecuteAsync()
        {
            using (HttpClient hc = new HttpClient())
                return (ParseSearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/all&page=" + page)).Content.ReadAsStringAsync())));
        }

        private Search.SearchResult ParseSearchResult(dynamic json)
        {
            return (new Search.SearchResult(json, max));
        }

        private int page;
        private int? max;
    }
}
