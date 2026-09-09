using System.Diagnostics;
using System.Security.Principal;

namespace TempCleaner;

public static class InitializerService
{
    public static void CheckLogFile()
    {
        if (!File.Exists(PathsService.LogPath))
        {
            File.Create(PathsService.LogPath).Dispose();
        }
    }


    public static bool IsAdmin()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }


    public static void ElevateToAdmin()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = Process.GetCurrentProcess().MainModule!.FileName,
            UseShellExecute = true,
            Verb = "runas"
        };

        try
        {
            Process.Start(startInfo);
        }
        catch
        {
            Console.WriteLine("Permissão de administrador negada.");
        }
    }
}