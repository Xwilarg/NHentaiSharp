using NHentaiSharp.Exception;

namespace NHentaiSharp.Search
{
    public struct SearchResult
    {
        public SearchResult(dynamic json)
        {
            if (json.error != null || json.result.Count == 0)
                throw new InvalidArgumentException();
            elements = new GalleryElement[json.result.Count];
            for (int i = 0; i < json.result.Count; i++)
                elements[i] = new GalleryElement(json.result[i]);
            numPages = json.num_pages;
            numPerPage = json.per_page;
        }

        public readonly GalleryElement[] elements;
        public readonly int numPages;
        public readonly int numPerPage;
    }
}
