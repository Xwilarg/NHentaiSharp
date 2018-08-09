using System;

namespace NHentaiSharp.Search
{
    public struct SearchResult
    {
        public SearchResult(dynamic json)
        {
            dynamic elem = json.result[0];
            id = elem.id;
            mediaId = elem.media_id;
            englishTitle = elem.title.english;
            japaneseTitle = elem.title.japanese;
            prettyTitle = elem.title.pretty;
            pages = new PageResult[elem.images.pages.Count];
            for (int i = 0; i < elem.images.pages.Count; i++)
                pages[i] = new PageResult(elem.images.pages[i]);
            cover = new PageResult(elem.images.cover);
            thumbnail = new PageResult(elem.images.thumbnail);
            numPages = elem.num_pages;
            scanlator = elem.scanlator;
            uploadDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)elem.upload_date);
            tags = new TagResult[elem.tags.Count];
            for (int i = 0; i < elem.tags.Count; i++)
                tags[i] = new TagResult(elem.tags[i]);
            numFavorites = elem.num_favorites;
        }

        public readonly long id;
        public readonly string mediaId;
        public readonly string englishTitle;
        public readonly string japaneseTitle;
        public readonly string prettyTitle;
        public readonly PageResult[] pages;
        public readonly PageResult cover;
        public readonly PageResult thumbnail;
        public readonly int numPages;
        public readonly string scanlator;
        public readonly DateTime uploadDate;
        public readonly TagResult[] tags;
        public readonly int numFavorites;
    }
}
