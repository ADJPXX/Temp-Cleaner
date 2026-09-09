namespace TempCleaner;

public static class Program
{
    public static void Main(string[] args)
    {
        if (!InitializerService.IsAdmin())
        {
            InitializerService.ElevateToAdmin();
            return;
        }

        InitializerService.CheckLogFile();
        
        LogService.StartExecution("ARQUIVOS %TEMP%");
        
        CleanService.ClearFiles(PathsService.TempPorcentagem);
        
        LogService.EndExecution();
        
        LogService.StartExecution("PASTAS %TEMP%");
        
        CleanService.ClearFolders(PathsService.TempPorcentagem);

        LogService.EndExecution();
        
        LogService.StartExecution("ARQUIVOS TEMP");
        
        CleanService.ClearFiles(PathsService.PathTemp);
        
        LogService.EndExecution();
        
        LogService.StartExecution("PASTAS TEMP");
        
        CleanService.ClearFolders(PathsService.PathTemp);
        
        LogService.StartExecution("PASTA SCREENSHOTS");

        CleanService.ClearScreenshots(PathsService.PastaScreenshots);
        
        LogService.EndExecution();

        LogService.StartExecution("LIXEIRAS");
        
        CleanService.ClearRecycleBin();
        
        LogService.EndExecution();
    }
}