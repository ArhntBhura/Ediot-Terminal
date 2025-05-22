
using System.Diagnostics;

public class ShellExecutor
{
    internal void Execute(string input)
    {
        ProcessStartInfo psi = new()
        {
            FileName = Environment.OSVersion.Platform == PlatformID.Win32NT ? "cmd.exe" : "/bin/bash",
            Arguments = (Environment.OSVersion.Platform == PlatformID.Win32NT ? "/c " : "-c ") + $"\"{input}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
                Console.Write(output);

            if (!string.IsNullOrEmpty(error))
                Console.Write(error);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {e.Message}");
            Console.ResetColor();
        }
    }
}