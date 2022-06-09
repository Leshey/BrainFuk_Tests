namespace BrainFuck;

public class ConsoleCursorWrapper : ICursorWrapper
{
    public void SetCursorPosition(int left, int top)
    {
        Console.SetCursorPosition(left, top);
    }
}