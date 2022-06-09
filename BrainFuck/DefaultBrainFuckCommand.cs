namespace BrainFuck
{
    public class DefaultBrainFuckCommand : ICommand
    {
        private readonly IInputOutput _inputOutput;

        public DefaultBrainFuckCommand(IInputOutput inputOutput) 
        {
            _inputOutput = inputOutput;
        }
        public void Execute()
        {
            Console.Clear();

            Repository BrainFuckCode = new Repository();
            DataOperations dataOperations = new DataOperations(BrainFuckCode, _inputOutput);
            dataOperations.EnumСodeBrainFuck(BrainFuckCode.Program);

            Console.WriteLine("\nPress any button.");
            Console.ReadKey(true);
        }
    }
}
