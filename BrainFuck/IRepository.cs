namespace BrainFuck;
public interface IRepository
{
    char[] Memory { get; set; }
    int Current { get; set; }
    string Program { get; set; }
}
