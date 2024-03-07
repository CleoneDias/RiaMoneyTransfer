using RiaMoneyTransfer.DataAccess.Models;

namespace RiaMoneyTransfer.DataAccess.Data
{
    public interface ICustomerData
    {
        Task<List<Customer>> GetCustomerAsync(bool sort = false);
        Task InsertCustomerAsync(List<Customer> customers);
    }
}