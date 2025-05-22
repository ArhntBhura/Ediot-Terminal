using System.Diagnostics;

public class BuiltInCommandHandler
{
    public bool TryHandle(string input)
    {
        string cmd = input.Trim().ToLower();

        if (cmd == "clear")
        {
            Console.Clear();
            return true;
        }

        if (cmd.StartsWith("cd "))
        {
            ChangeDirectory(input.Substring(3).Trim());
            return true;
        }

        return false;
    }

    private void ChangeDirectory(string path)
    {
        try
        {
            string targetPath = Path.GetFullPath(path, Directory.GetCurrentDirectory());

            if (Directory.Exists(targetPath))
                Directory.SetCurrentDirectory(targetPath);
            else
                Console.WriteLine($"Error: Directory not found: {path}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to change directory: {e.Message}");
        }
    }
}