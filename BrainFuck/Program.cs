namespace BrainFuck;
public static class Program
{
    public static void Main()
    {
        var consoleCursorWrapper = new ConsoleCursorWrapper();
        var inputOutput = new InputOutput(Console.In, Console.Out, consoleCursorWrapper);
        var menu = new Menu(inputOutput, inputOutput);

        menu.RunMenu();
    }
}

public interface IMenuTextWriter 
{
    void PrintMenu(MenuLine[] menuLines, int selectedMenuIndex);
}

public class Menu 
{
    private readonly IMenuTextWriter _menuTextWriter;
    private readonly IInputOutput _inputOutput;

    public Menu(IMenuTextWriter menuTextWriter, IInputOutput inputOutput) 
    {
        _menuTextWriter = menuTextWriter;
        _inputOutput = inputOutput;
    }

    public void RunMenu() 
    {
        var exitToken = new ExitToken();

        var menuLines = new[] { new MenuLine("Run the \"Hello World\" program and go fuck yourself!", new DefaultBrainFuckCommand(_inputOutput)),
                                new MenuLine("Do Default command and go fuck yourself!", new DefaultCommand()),
                                new MenuLine("Run your program and go fuck yourself!", new EnterAndExecuteProgramCommand(_inputOutput)),
                                new MenuLine("Exit. And you may want to go and fuck yourself!", new ExitCommand(exitToken))
        };
        var menuIndex = 0;

        _menuTextWriter.PrintMenu(menuLines, menuIndex);

        while (true)
        {
            var consoleKeyInfo = Console.ReadKey(true);
            if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
            {
                menuIndex -= 1;
                menuIndex = menuIndex < 0 ? 0 : menuIndex;
            }
            else if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
            {
                menuIndex += 1;
                menuIndex = menuIndex >= menuLines.Length ? menuLines.Length - 1 : menuIndex;
            }
            else if (consoleKeyInfo.Key == ConsoleKey.Enter)
            {
                var item = menuLines[menuIndex];
                item.Execute();
                if (exitToken.IsCanceled == false)
                {
                    Console.Clear();
                }
            }

            if (exitToken.IsCanceled)
            {
                return;
            }

            _menuTextWriter.PrintMenu(menuLines, menuIndex);
        }

    }
}