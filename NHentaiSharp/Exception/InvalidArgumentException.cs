namespace NHentaiSharp.Exception
{
    public class InvalidArgumentException : System.Exception
    {
        public InvalidArgumentException() : base("Your search didn't return any result, that probably mean one of the argument your provided is invalid")
        { }
    }
}
