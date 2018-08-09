namespace NHentaiSharp.Search
{
    public struct PageResult
    {
        public PageResult(dynamic json)
        {
#if DEBUG
            if (json.t != "j")
                throw new System.ArgumentException("Invalid t value: " + json.t);
#endif
            width = json.w;
            height = json.h;
        }

        public readonly int width;
        public readonly int height;
    }
}
