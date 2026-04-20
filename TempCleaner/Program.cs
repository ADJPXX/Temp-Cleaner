using System.Diagnostics;

namespace TempCleaner;

class Program
{
    private static ProcessStartInfo startInfo = new();
    
    private static void Main(string[] args)
    {
        string tempPorcentagem = Path.GetTempPath();
        
        string temp = "Temp";
        
        string windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        
        string pathTemp = Path.Combine(windows, temp);
        
        string pastaImagens = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        string pastaScreenshots = Path.Combine(pastaImagens, "Screenshots");

        LimparArquivos(tempPorcentagem);
        LimparPastas(tempPorcentagem);

        LimparArquivos(pathTemp);
        LimparPastas(pathTemp);
        
        LimparLixeira();

        LimparScreenshots(pastaScreenshots);
    }

    
    private static void LimparArquivos(string temp)
    {
        foreach (string file in Directory.GetFiles(temp))
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
        foreach (string file in Directory.GetDirectories(temp))
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
            startInfo.FileName = "cmd.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "Clear-RecycleBin -Force";

            Process.Start(startInfo);
            
            
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                startInfo.FileName = "cmd.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = $"/c rd /s /q {drive}$RECYCLE.BIN";

                Process.Start(startInfo);
            }
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
            startInfo.FileName = "cmd.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = $"/c rd /s /q {screenshots}";

            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FALHA AO DELETAR A PASTA DE SCREENSHOTS. ERRO: {ex.Message}");
        }
    }
}