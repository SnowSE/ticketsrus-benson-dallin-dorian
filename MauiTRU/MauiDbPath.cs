namespace MauiTRU
{
    public class MauiDbPath : IDbPath
    {
        public string Directory => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}
