using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public static class DeckReader
{
    public static string[] ReadDeckInfoFromFile(string path)
    {
        string[] lines;

        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (Exception)
        {

            throw;
        }

        return lines;
    }
}