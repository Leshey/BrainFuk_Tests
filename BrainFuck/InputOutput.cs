namespace BrainFuck;
public class InputOutput : IInputOutput, IMenuTextWriter
{
    private TextReader Reader;
    private TextWriter Writer;
    private ICursorWrapper _cursorWrapper;

    public InputOutput(TextReader output, TextWriter input, ICursorWrapper cursorWrapper)
    {
        Reader = output;
        Writer = input;
        _cursorWrapper = cursorWrapper;
    }

    public InputOutput(TextReader output, TextWriter input)
    {
        Reader = output;
        Writer = input;
    }

    public char GetCharUser()
    {
        while (true)
        {
            string userInput = GetStringUser();
            if (char.TryParse(userInput, out char result))
            {
                return result;
            }
        }
    }
    public string GetStringUser()
    {
        return Reader.ReadLine();
    }
    public void OutputConsole(string messageOrChar)
    {
        Writer.Write(messageOrChar);
    }
    
    public void PrintMenu(MenuLine[] menuLines, int selectedMenuIndex) 
    {
        _cursorWrapper.SetCursorPosition(0, 0);
        var index = 0;
        foreach (var menuLine in menuLines)
        {
            if (index == selectedMenuIndex)
            {
                Console.Write(">");
            }
            else
            {
                Console.Write(" ");
            }

            Console.Write(menuLine.Name);
            Console.Write("\n");
            index += 1;
        }
    }

}
