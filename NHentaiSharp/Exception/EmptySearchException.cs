namespace NHentaiSharp.Exception
{
    public class EmptySearchException : System.Exception
    {
        public EmptySearchException() : base("You need to provide a search query.")
        { }
    }
}
