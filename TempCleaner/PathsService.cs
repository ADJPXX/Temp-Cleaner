namespace TempCleaner;

public static class PathsService
{
    public static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempCleaner.log");
    
    public static readonly string TempPorcentagem = Path.GetTempPath();

    private const string Temp = "Temp";

    private static readonly string Windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        
    public static readonly string PathTemp = Path.Combine(Windows, Temp);

    private static readonly string PastaImagens = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

    public static readonly string PastaScreenshots = Path.Combine(PastaImagens, "Screenshots");
}