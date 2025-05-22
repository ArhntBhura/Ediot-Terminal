
using System.Text;

public class CommandHistory
{
    private readonly List<string> history = new();
    private int historyIndex = -1;

    public void Add(string command)
    {
        history.Add(command);
        historyIndex = history.Count;
    }

    public string ReadInputWithHistory(string displayDir, string userName)
    {
        var buffer = new StringBuilder();
        ConsoleKeyInfo key;

        while (true)
        {
            key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return buffer.ToString();
            }
            else if (key.Key == ConsoleKey.Backspace && buffer.Length > 0)
            {
                buffer.Length--;
                Console.Write("\b \b");
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (history.Count > 0 && historyIndex > 0)
                {
                    historyIndex--;
                    UpdateBuffer(buffer, displayDir, userName);
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (historyIndex < history.Count - 1)
                {
                    historyIndex++;
                    UpdateBuffer(buffer, displayDir, userName, clearOnly: true);
                }
                else
                {
                    historyIndex = history.Count;
                    UpdateBuffer(buffer, displayDir, userName, clearOnly: true);
                }
            }
            else
            {
                buffer.Append(key.KeyChar);
                Console.Write(key.KeyChar);
            }
        }
    }

    private void UpdateBuffer(StringBuilder buffer, string displayDir, string userName, bool clearOnly = false)
    {
        Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(userName);
        Console.ResetColor();
        Console.Write(" ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("➜ ");
        Console.ResetColor();
        Console.Write($"{displayDir} ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("$ ");
        Console.ResetColor();

        buffer.Clear();
        if (!clearOnly && historyIndex < history.Count)
        {
            buffer.Append(history[historyIndex]);
            Console.Write(buffer.ToString());
        }
    }
}