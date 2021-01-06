using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuestMania
{
    public static class FileManager
    {
        public static byte[,] LoadLevelData(string filename)
        {
            using (var streamReader = new StreamReader(filename))
            {
                var serializer = new JsonSerializer();
                return (byte[,])serializer.Deserialize(streamReader, typeof(byte[,]));
            }
        }

        public static string GetPath(string pathFromProject)
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"..\..\..\{ pathFromProject }"));
        }
    }
}
