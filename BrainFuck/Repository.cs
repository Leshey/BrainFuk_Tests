namespace BrainFuck;
public class Repository : IRepository
{
    public char[] Memory { get; set; }
    public int Current { get; set; }
    public string Program { get; set; }
    public Repository()
    {
        Memory = new char[30000];
        Current = 0;
        Program = "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.";
    }
}
