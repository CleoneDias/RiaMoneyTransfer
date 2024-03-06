using Newtonsoft.Json;
using System.IO;

namespace RiaMoneyTransfer.DataAccess.FileAccess
{
    public class ReadWriteAccess : IReadWriteAccess
    {
        public async Task<List<T>> ReadDataAsync<T>()
        {
            try
            {
                var path = GetFilePath();
                if (!File.Exists(path))
                {
                    throw new Exception("No customers found.");
                }
                using (var reader = new StreamReader(path))
                {
                    return JsonConvert.DeserializeObject<List<T>>(await reader.ReadToEndAsync());
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task WriteDataAsync(string data)
        {
            try
            {
                var path = GetFilePath();
                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }
                using (var writer = new StreamWriter(path))
                {
                    await writer.WriteLineAsync(data);
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string GetFilePath()
        {
            var directory = Directory.GetCurrentDirectory();
            var path = directory.Substring(0, directory.LastIndexOf(@"RiaMoneyTransfer") + 16);
            return path + @"\temp.txt";
        }
    }
}
