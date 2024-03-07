using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.IO;

namespace RiaMoneyTransfer.DataAccess.FileAccess
{
    public class ReadWriteAccess : IReadWriteAccess
    {
        private static object _lock = new object();

        public Task<List<T>> ReadDataAsync<T>()
        {
            var returnList = new List<T>();
            try
            {
                var path = GetFilePath();
                if (!File.Exists(path))
                {
                    return Task.FromResult(returnList);
                }
                lock (_lock)
                {
                    using (var reader = new StreamReader(path))
                    {
                        returnList = JsonConvert.DeserializeObject<List<T>>($"[{reader.ReadToEnd()}]");
                        reader.Close();
                        return Task.FromResult(returnList);
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task WriteDataAsync(string data)
        {
            try
            {
                var path = GetFilePath();
                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }
                lock (_lock)
                {
                    using (var writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(data + ",");
                        writer.Close();
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Task.CompletedTask;
        }

        private string GetFilePath()
        {
            var directory = Directory.GetCurrentDirectory();
            var path = directory.Substring(0, directory.LastIndexOf(@"RiaMoneyTransfer") + 16);
            return path + @"\temp.txt";
        }
    }
}
