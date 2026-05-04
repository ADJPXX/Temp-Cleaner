using System.Diagnostics;

namespace TempCleaner;

public static class Program
{
    private static readonly ProcessStartInfo StartInfo = new();
    
    public static void Main(string[] args)
    {
        var tempPorcentagem = Path.GetTempPath();
        
        const string temp = "Temp";
        
        var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        
        var pathTemp = Path.Combine(windows, temp);
        
        var pastaImagens = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        var pastaScreenshots = Path.Combine(pastaImagens, "Screenshots");
        
        LimparArquivos(tempPorcentagem);
        LimparPastas(tempPorcentagem);

        LimparArquivos(pathTemp);
        LimparPastas(pathTemp);
        
        LimparScreenshots(pastaScreenshots);
        
        LimparLixeira();
    }

    
    private static void LimparArquivos(string temp)
    {
        foreach (var file in Directory.GetFiles(temp))
        {
            try
            {
                Console.WriteLine($"DELETANDO O ARQUIVO: {file}");
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
                Console.WriteLine("ARQUIVO DELETADO!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FALHA AO DELETAR O ARQUIVO: {file}");
                Console.WriteLine($"ERRO: {ex.Message}\n");
            }
        }
    }


    private static void LimparPastas(string temp)
    {
        foreach (var file in Directory.GetDirectories(temp))
        {
            try
            {
                Console.WriteLine($"DELETANDO A PASTA: {file}");
                Directory.Delete(file, true);
                Console.WriteLine("PASTA DELETADA!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FALHA AO DELETAR A PASTA: {file}");
                Console.WriteLine($"ERRO: {ex.Message}\n");
            }
        }
    }


    private static void LimparLixeira()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                StartInfo.FileName = "cmd.exe";
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                StartInfo.Arguments = $"/c rd /s /q {drive}$RECYCLE.BIN";

                Process.Start(StartInfo);
            }
            
            StartInfo.FileName = "cmd.exe";
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            StartInfo.Arguments = "Clear-RecycleBin -Force";

            Process.Start(StartInfo);
        }

        catch (Exception ex)
        {
            Console.WriteLine($"FALHA AO DELETAR O ARQUIVO! ERRO: {ex.Message}");
        }
    }

    
    private static void LimparScreenshots(string screenshots)
    {
        try
        {
            StartInfo.FileName = "cmd.exe";
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            StartInfo.Arguments = $"/c rd /s /q {screenshots}";

            Process.Start(StartInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FALHA AO DELETAR A PASTA DE SCREENSHOTS. ERRO: {ex.Message}");
        }
    }
}