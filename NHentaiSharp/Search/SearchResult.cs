namespace NHentaiSharp.Search
{
    public struct SearchResult
    {
        public SearchResult(dynamic json, int? max)
        {
            if (!max.HasValue || max > json.result.Count)
                max = json.result.Count;
            if (json.result.Count < max)
                max = json.result.Count;
            elements = new GalleryElement[max.Value];
            for (int i = 0; i < max; i++)
                elements[i] = new GalleryElement(json.result[i]);
            numPages = json.num_pages;
            numPerPage = json.per_page;
        }

        public readonly GalleryElement[] elements;
        public readonly int numPages;
        public readonly int numPerPage;
    }
}
