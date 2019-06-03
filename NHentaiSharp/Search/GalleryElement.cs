using System;

namespace NHentaiSharp.Search
{
    public struct GalleryElement
    {
        public GalleryElement(dynamic json)
        {
            id = json.id;
            mediaId = json.media_id;
            englishTitle = json.title.english;
            japaneseTitle = json.title.japanese;
            prettyTitle = json.title.pretty;
            pages = new Page[json.images.pages.Count];
            for (int i = 0; i < json.images.pages.Count; i++)
                pages[i] = new Page(json.images.pages[i],
                    "https://i.nhentai.net/galleries/" + mediaId + "/" + (i + 1) + ".",
                    "https://t.nhentai.net/galleries/" + mediaId + "/" + (i + 1) + "t.");
            cover = new Page(json.images.cover, "https://t.nhentai.net/galleries/" + mediaId + "/cover.");
            thumbnail = new Page(json.images.thumbnail, "https://t.nhentai.net/galleries/" + mediaId + "/thumb.");
            numPages = json.num_pages;
            scanlator = json.scanlator;
            uploadDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)json.upload_date);
            tags = new Tag[json.tags.Count];
            for (int i = 0; i < json.tags.Count; i++)
                tags[i] = new Tag(json.tags[i]);
            numFavorites = json.num_favorites;
        }

        public readonly long id;
        public readonly string mediaId;
        public readonly string englishTitle;
        public readonly string japaneseTitle;
        public readonly string prettyTitle;
        public readonly Page[] pages;
        public readonly Page cover;
        public readonly Page thumbnail;
        public readonly int numPages;
        public readonly string scanlator;
        public readonly DateTime uploadDate;
        public readonly Tag[] tags;
        public readonly int numFavorites;
    }
}
