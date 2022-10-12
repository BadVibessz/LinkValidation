namespace Core;

public static class FileManager
{
    public static void WriteLinesIntoFile(string filename, List<string> lines)
    {
        try
        {
            var writer = new StreamWriter(filename);
            foreach (var line in lines)
                writer.WriteLine(line);
            writer.Flush();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}