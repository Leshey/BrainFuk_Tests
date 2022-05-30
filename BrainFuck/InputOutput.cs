namespace BrainFuck;
public class InputOutput : IInputOutput
{
    private TextReader Reader;
    private TextWriter Writer;

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
}
