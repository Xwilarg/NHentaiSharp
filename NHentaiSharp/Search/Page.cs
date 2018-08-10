namespace NHentaiSharp.Search
{
    public struct Page
    {
        public Page(dynamic json)
        {
            switch (json.t.ToString())
            {
                case "j":
                    format = PageFormat.JPG;
                    break;

                case "p":
                    format = PageFormat.PNG;
                    break;

                default:
                    throw new System.ArgumentException("Invalid format '" + json.t + "'");
            }
            width = json.w;
            height = json.h;
        }

        public readonly int width;
        public readonly int height;
        public readonly PageFormat format;
    }
}
