namespace BrainFuck;

public class DataOperations
{
    private IRepository _brainFuckCode;
    private IInputOutput _inputOutput;

    public DataOperations(IRepository brainFuckCode, IInputOutput inputOutput)
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
