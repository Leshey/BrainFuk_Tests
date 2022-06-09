namespace BrainFuck;
public class DefaultCommand : ICommand
{
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Not From Brainfuck Hello World!");

        Console.WriteLine("\nPress any button.");
        Console.ReadKey(true);
    }
}
