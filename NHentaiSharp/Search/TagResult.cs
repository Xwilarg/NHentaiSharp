namespace NHentaiSharp.Search
{
    public struct TagResult
    {
        public TagResult(dynamic json)
        {
            id = json.id;
            name = json.name;
            url = json.url;
            count = json.count;
            switch (json.type.ToString())
            {
                case "tag":
                    type = TagType.Tag;
                    break;

                case "character":
                    type = TagType.Character;
                    break;

                case "language":
                    type = TagType.Language;
                    break;

                case "parody":
                    type = TagType.Parody;
                    break;

                case "category":
                    type = TagType.Category;
                    break;

                case "artist":
                    type = TagType.Artist;
                    break;

                case "group":
                    type = TagType.Group;
                    break;

                default:
                    throw new System.ArgumentException("Invalid type '" + json.type + "'");
            }
        }

        public readonly int id;
        public readonly TagType type;
        public readonly string name;
        public readonly string url;
        public readonly int count;
    }
}
