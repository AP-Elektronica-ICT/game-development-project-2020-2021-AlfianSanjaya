using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace QuestMania.FileSystem
{
    public static class FileManager
    {
        public static char[,] LoadLevelData(string path)
        {
            int width = 0;
            int height = 0;

            // Read the file
            List<string> lines = File.ReadAllLines(path).ToList();

            // Get dimensions of the map
            foreach (var line in lines)
            {
                width = line.Length;
                height++;
            }

            // Populate the 2d-char array
            char[,] output = new char[height, width];
            int rows = 0;
            foreach (var line in lines)
            {
                for (int col = 0; col < width; col++)
                {
                    output[rows, col] = line[col];
                }
                rows++;
            }
            return output;
        }

        public static string GetPath(string pathFromProject)
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"..\..\..\{ pathFromProject }"));
        }
    }
}
