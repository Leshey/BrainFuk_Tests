namespace BrainFuck;
public interface IInputOutput
{
    char GetCharUser();
    string GetStringUser();
    void OutputConsole(string messageOrChar);
}