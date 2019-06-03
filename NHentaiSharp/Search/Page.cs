using System;

namespace NHentaiSharp.Search
{
    public struct Page
    {
        public Page(dynamic json, string urlBase, string urlPreview)
        {
            switch (json.t.ToString())
            {
                case "j":
                    format = PageFormat.JPG;
                    urlBase += "jpg";
                    urlPreview += "jpg";
                    break;

                case "p":
                    format = PageFormat.PNG;
                    urlBase += "png";
                    urlPreview += "png";
                    break;

                default:
                    throw new ArgumentException("Invalid format '" + json.t + "'");
            }
            imageUrl = new Uri(urlBase);
            previewUrl = new Uri(urlPreview);
            width = json.w;
            height = json.h;
        }

        public Page(dynamic json, string urlBase)
        {
            switch (json.t.ToString())
            {
                case "j":
                    format = PageFormat.JPG;
                    urlBase += "jpg";
                    break;

                case "p":
                    format = PageFormat.PNG;
                    urlBase += "png";
                    break;

                default:
                    throw new ArgumentException("Invalid format '" + json.t + "'");
            }
            imageUrl = new Uri(urlBase);
            previewUrl = null;
            width = json.w;
            height = json.h;
        }

        public readonly int width;
        public readonly int height;
        public readonly PageFormat format;
        public readonly Uri imageUrl;
        public readonly Uri previewUrl;
    }
}
