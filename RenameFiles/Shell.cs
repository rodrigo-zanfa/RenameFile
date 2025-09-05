using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RenameFiles
{
    public static class Shell
    {
        public static (int ExitCode, string Output, string Error) Execute(this string cmd)
        {
            var escapedArgs = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? cmd.Replace("\"", "\\\"") : cmd;

            var process = new Process();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
            }

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            return (process.ExitCode, output, error);
        }
    }
}
