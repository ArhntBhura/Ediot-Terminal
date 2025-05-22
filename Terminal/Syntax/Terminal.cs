public class Terminal
{
    private readonly CommandHistory history = new();
    private readonly BuiltInCommandHandler builtInHandler = new();
    private readonly ShellExecutor shellExecutor = new();

    public void Start()
    {
        string userName = Environment.UserName;
        string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        Console.WriteLine($"Welcome {userName}!");

        while (true)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string displayDir = currentDir.StartsWith(homeDir) ? "~" + currentDir.Substring(homeDir.Length) : currentDir;

            DisplayPrompt(userName, displayDir);

            string input = history.ReadInputWithHistory(displayDir, userName);

            if (string.IsNullOrWhiteSpace(input)) continue;
            if (input.Trim().ToLower() == "exit") break;

            history.Add(input);

            if (builtInHandler.TryHandle(input)) continue;

            shellExecutor.Execute(input);
        }
    }

    private void DisplayPrompt(string userName, string displayDir)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{userName}");
        Console.ResetColor();
        Console.Write(" ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"➜ ");
        Console.ResetColor();
        Console.Write($"{displayDir}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"@ ");
        Console.ResetColor();
    }
}