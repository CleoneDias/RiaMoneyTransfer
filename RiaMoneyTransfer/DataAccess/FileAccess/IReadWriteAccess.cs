namespace RiaMoneyTransfer.DataAccess.FileAccess
{
    public interface IReadWriteAccess
    {
        Task<List<T>> ReadDataAsync<T>();
        Task WriteDataAsync(string data);
    }
}