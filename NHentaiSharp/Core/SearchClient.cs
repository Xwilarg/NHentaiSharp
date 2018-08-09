using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace NHentaiSharp.Core
{
    public class SearchClient
    {
        public SearchClient(params string[] tags)
        {
            this.tags = string.Join("%20", tags);
            page = 1;
        }

        public SearchClient WithPage(int page)
        {
            this.page = page;
            return (this);
        }

        public SearchClient WithTags(params string[] tags)
        {
            this.tags = string.Join("%20", tags);
            return (this);
        }

        public SearchClient WithMax(int? max)
        {
            this.max = max;
            return (this);
        }

        public async Task<Search.SearchResult> ExecuteAsync()
        {
            if (string.IsNullOrEmpty(tags))
                throw new Exception.EmptySearchException();
            using (HttpClient hc = new HttpClient())
                return (new Search.SearchResult(JsonConvert.DeserializeObject(await(await hc.GetAsync("https://nhentai.net/api/galleries/search?query=" + tags + "&page=" + page)).Content.ReadAsStringAsync())));
        }

        private int page;
        private string tags;
        private int? max;
    }
}
