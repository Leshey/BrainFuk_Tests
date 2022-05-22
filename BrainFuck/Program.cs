public static class Program
{
    public static void Main()
    {
        Repository BrainFuckCode = new Repository();
        DataOperations dataOperations = new DataOperations(BrainFuckCode, new InputOutput(Console.In, Console.Out));
        dataOperations.EnumСodeBrainFuck(BrainFuckCode.Program);
    }
}

public class Repository
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
public class DataOperations
{
    private Repository _brainFuckCode;
    private InputOutput _inputOutput;

    public DataOperations(Repository brainFuckCode, InputOutput inputOutput)
    {
        _brainFuckCode = brainFuckCode;
        _inputOutput = inputOutput;
    }

    public void EnumСodeBrainFuck(string brainFuckCode)
    {
        for (int i = 0; i < brainFuckCode.Length; i++)
        {
            switch (brainFuckCode[i])
            {
                case '+':
                    NextCharValue();
                    break;
                case '-':
                    PreviousCharValue();
                    break;
                case '.':
                    DisplayCellValue();
                    break;
                case '>':
                    NextCell();
                    break;
                case '<':
                    PreviusCell();
                    break;
                case ',':
                    InputValueInCell();
                    break;
                case '[':
                    i = IfZeroNext(i, brainFuckCode);
                    break;
                case ']':
                    i = IfNoZeroBack(i, brainFuckCode);
                    break;
            }
        }
    }
    public virtual void NextCharValue()
    {
        if (_brainFuckCode.Memory[_brainFuckCode.Current] < char.MaxValue)
        {
            _brainFuckCode.Memory[_brainFuckCode.Current]++;
        }
    }

    public virtual void PreviousCharValue()
    {
        if (_brainFuckCode.Memory[_brainFuckCode.Current] > char.MinValue) 
        {
            _brainFuckCode.Memory[_brainFuckCode.Current]--;
        }
    }
    public virtual void DisplayCellValue()
    {
        _inputOutput.OutputConsole(Convert.ToString(_brainFuckCode.Memory[_brainFuckCode.Current]));
    }
    public virtual void NextCell()
    {
        if (_brainFuckCode.Current<_brainFuckCode.Memory.Length)
        {
            _brainFuckCode.Current = _brainFuckCode.Current + 1;
        }
    }
    public virtual void PreviusCell()
    {
        if (_brainFuckCode.Current>0)
        {
            _brainFuckCode.Current = _brainFuckCode.Current - 1;
        }
    }
    public virtual void InputValueInCell()
    {
        _brainFuckCode.Memory[_brainFuckCode.Current] = _inputOutput.GetCharUser();
    }
    public virtual int IfZeroNext(int PositionNumber, string brainFuckCode)
    {
        if (_brainFuckCode.Memory[_brainFuckCode.Current] == 0)
        {
            int NumberOfopenBrackets = 0;
            PositionNumber += 1;
            while (brainFuckCode[PositionNumber] != ']' || NumberOfopenBrackets != 0)
            {
                if (brainFuckCode[PositionNumber] == '[')
                {
                    NumberOfopenBrackets += 1;
                    PositionNumber += 1;
                }
                if (brainFuckCode[PositionNumber] == ']')
                {
                    NumberOfopenBrackets -= 1;
                    PositionNumber += 1;
                }
                PositionNumber += 1;
            }
        }
        return PositionNumber;
    }
    public virtual int IfNoZeroBack(int PositionNumber, string brainFuckCode)
    {
        if (_brainFuckCode.Memory[_brainFuckCode.Current] != 0)
        {
            int NumberOfopenBrackets = 0;
            PositionNumber -= 1;
            while (brainFuckCode[PositionNumber] != '[' || NumberOfopenBrackets != 0)
            {
                if (brainFuckCode[PositionNumber] == ']')
                {
                    NumberOfopenBrackets += 1;
                    PositionNumber -= 1;
                }
                if (brainFuckCode[PositionNumber] == '[')
                {
                    NumberOfopenBrackets -= 1;
                    PositionNumber -= 1;
                }
                PositionNumber -= 1;
            }
        }
        return PositionNumber;
    }
}

public class InputOutput
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