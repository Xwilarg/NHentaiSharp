using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NHentaiSharp.Core
{
    public static class SearchClient
    {
        /// <summary>
        /// Get a tag for an exact search
        /// </summary>
        public static string GetExactTag(string tag)
            => "\"" + tag + "\"";

        /// <summary>
        /// Get a tag for a specific category
        /// </summary>
        public static string GetCategoryTag(string tag, Search.TagType type)
            => type.ToString().ToLower().Replace("y", "ie") + "s:" + tag;

        /// <summary>
        /// Get a tag to exclude
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetExcludeTag(string tag)
            => "-" + tag;

        /// <summary>
        /// Search with specifics tags
        /// </summary>
        public static async Task<Search.SearchResult> SearchWithTagsAsync(params string[] tags)
            => await SearchWithTagsAsync(tags, 1);
        public static async Task<Search.SearchResult> SearchWithTagsAsync(string[] tags, int page)
        {
            string allTags = Uri.EscapeDataString(string.Join(" ", tags));
            if (string.IsNullOrEmpty(allTags))
                throw new Exception.EmptySearchException();
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await(await hc.GetAsync("https://nhentai.net/api/galleries/search?query=" + allTags + "&page=" + page)).Content.ReadAsStringAsync())));
        }

        /// <summary>
        /// Search with a specific tag id
        /// </summary>
        public static async Task<Search.SearchResult> SearchByTagIdAsync(int id, int page = 1)
        {
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/tagged?tag_id=" + id + "&page=" + page)).Content.ReadAsStringAsync())));
        }

        /// <summary>
        /// Search for a doujinshi given it id
        /// </summary>
        public static async Task<Search.GalleryElement> SearchByIdAsync(int id)
        {
            using (HttpClient hc = new HttpClient())
                return (new Search.GalleryElement(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/gallery/" + id)).Content.ReadAsStringAsync())));
        }

        /// <summary>
        /// Search without any tag
        /// </summary>
        public static async Task<Search.SearchResult> SearchAsync(int page = 1)
        {
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await (await hc.GetAsync("https://nhentai.net/api/galleries/all?page=" + page)).Content.ReadAsStringAsync())));
        }
    }
}
