using System.Diagnostics;

namespace TempCleaner;

public static class CleanService
{
    public static void ClearFiles(string temp)
    {
        foreach (var file in Directory.GetFiles(temp))
        {
            try
            {
                LogService.AddLog($"ARQUIVO ALVO: {file}");
                
                Console.WriteLine($"ARQUIVO ALVO: {file}");
                
                File.SetAttributes(file, FileAttributes.Normal);
                
                File.Delete(file);

                if (!File.Exists(file))
                {
                    LogService.AddLog("ARQUIVO DELETADO!\n");
                }
            }
            catch (Exception ex)
            {
                LogService.AddLog($"FALHA AO DELETAR O ARQUIVO, ERRO: {ex.Message}\n");
                
                Console.WriteLine($"FALHA AO DELETAR O ARQUIVO: {file}");
                
                Console.WriteLine($"ERRO: {ex.Message}\n");
            }
        }
    }


    public static void ClearFolders(string temp)
    {
        foreach (var file in Directory.GetDirectories(temp))
        {
            try
            {
                LogService.AddLog($"PASTA ALVO: {file}");
                
                Console.WriteLine($"PASTA ALVO: {file}");
                
                Directory.Delete(file, true);

                if (!Directory.Exists(file))
                {
                    LogService.AddLog("PASTA DELETADA!\n");
                }
            }
            catch (Exception ex)
            {
                LogService.AddLog($"FALHA AO DELETAR A PASTA, ERRO: {ex.Message}\n");
                
                Console.WriteLine($"FALHA AO DELETAR A PASTA: {file}");
                
                Console.WriteLine($"ERRO: {ex.Message}\n");
            }
        }
    }


    public static void ClearRecycleBin()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.Name.Contains(@"G:\"))
                {
                    continue;
                }

                var lixeiraDrive = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c rd /s /q \"{drive.Name}$RECYCLE.BIN\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                
                lixeiraDrive?.WaitForExit();

                if (lixeiraDrive?.ExitCode == 0)
                {
                    LogService.AddLog($"LIXEIRA DELETADA DO DRIVE: {drive.Name}");
                }
                else
                {
                    LogService.AddLog($"FALHA AO DELETAR A LIXEIRA DO DRIVE: {drive.Name} --- EXIT CODE: {lixeiraDrive?.ExitCode}\n");
                }
            }
        }

        catch (Exception ex)
        {
            LogService.AddLog($"FALHA AO DELETAR O ARQUIVO! ERRO: {ex.Message}\n");
            
            Console.WriteLine($"FALHA AO DELETAR O ARQUIVO! ERRO: {ex.Message}");
        }
    }


    public static void ClearScreenshots(string screenshots)
    {
        try
        {
            var pastaScreenhots = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C rd /s /q \"{screenshots}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            
            pastaScreenhots?.WaitForExit();

            if (!Directory.Exists(screenshots))
            {
                LogService.AddLog("PASTA \"Screenshots\" DELETADA COM SUCESSO!");
            }
            else
            {
                LogService.AddLog("PASTA \"Screenshots\" FALHOU AO DELETAR!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FALHA AO DELETAR A PASTA DE SCREENSHOTS. ERRO: {ex.Message}");
        }
    }
}