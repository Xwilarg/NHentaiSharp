namespace NHentaiSharp.Search
{
    public struct Page
    {
        public Page(dynamic json, string urlBase, string urlPreview)
        {
            imageUrl = urlBase;
            previewUrl = urlPreview;
            switch (json.t.ToString())
            {
                case "j":
                    format = PageFormat.JPG;
                    imageUrl += "jpg";
                    previewUrl += "jpg";
                    break;

                case "p":
                    format = PageFormat.PNG;
                    imageUrl += "png";
                    previewUrl += "png";
                    break;

                default:
                    throw new System.ArgumentException("Invalid format '" + json.t + "'");
            }
            width = json.w;
            height = json.h;
        }

        public Page(dynamic json, string urlBase)
        {
            imageUrl = urlBase;
            previewUrl = null;
            switch (json.t.ToString())
            {
                case "j":
                    format = PageFormat.JPG;
                    imageUrl += "jpg";
                    break;

                case "p":
                    format = PageFormat.PNG;
                    imageUrl += "png";
                    break;

                default:
                    throw new System.ArgumentException("Invalid format '" + json.t + "'");
            }
            width = json.w;
            height = json.h;
        }

        private void Init(dynamic json, bool havePreview)
        {

        }

        public readonly int width;
        public readonly int height;
        public readonly PageFormat format;
        public readonly string imageUrl;
        public readonly string previewUrl;
    }
}
