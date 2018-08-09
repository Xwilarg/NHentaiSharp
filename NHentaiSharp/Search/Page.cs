namespace NHentaiSharp.Search
{
    public struct Page
    {
        public Page(dynamic json)
        {
            t = json.t;
            width = json.w;
            height = json.h;
        }

        public readonly int width;
        public readonly int height;
        public readonly string t; // TODO: What is this thing ?
    }
}
