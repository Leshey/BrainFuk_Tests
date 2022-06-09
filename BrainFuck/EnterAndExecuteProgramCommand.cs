namespace BrainFuck;

public class EnterAndExecuteProgramCommand : ICommand
{
    private readonly IInputOutput _inputOutput;

    public EnterAndExecuteProgramCommand(IInputOutput inputOutput)
    {
        _inputOutput = inputOutput;
    }

    public void Execute()
    {
        Console.Clear();
        Repository BrainFuckCode = new Repository();

        var IO = _inputOutput;
        IO.OutputConsole("Type some program in BrainFuck and go fuck yourself!\n");

        BrainFuckCode.Program = IO.GetStringUser();
        DataOperations dataOperations = new DataOperations(BrainFuckCode, IO);
        dataOperations.EnumСodeBrainFuck(BrainFuckCode.Program);

        Console.WriteLine("\nPress any button.");
        Console.ReadKey(true);
    }

}

