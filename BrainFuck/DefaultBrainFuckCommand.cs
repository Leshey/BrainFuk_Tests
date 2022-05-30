using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BrainFuck
{
    public class DefaultBrainFuckCommand : ICommand 
    {
        public void Execute() 
        {
            Console.Clear();

            Repository BrainFuckCode = new Repository();
            DataOperations dataOperations = new DataOperations(BrainFuckCode, new InputOutput(Console.In, Console.Out));
            dataOperations.EnumСodeBrainFuck(BrainFuckCode.Program);

            Console.WriteLine("\nPress any button.");
            Console.ReadKey(true);
        }
    }
}
