namespace NHentaiSharp.Exception
{
    public class EmptySearchException : System.Exception
    {
        public EmptySearchException() : base("You need to provide tags before searching.")
        { }
    }
}
