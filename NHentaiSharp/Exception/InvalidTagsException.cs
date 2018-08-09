namespace NHentaiSharp.Exception
{
    public class InvalidTagsException : System.Exception
    {
        public InvalidTagsException() : base("There isn't any result with the tags you provided or you went too far in the pages.")
        { }
    }
}
