namespace DapperPatterns.People
{
    public class Name
    {
        public Name(string first, string last)
        {
            First = first;
            Last = last;
        }

        public string First { get; } 

        public string Last { get; }
    }
}
