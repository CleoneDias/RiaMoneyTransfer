using RiaMoneyTransfer.DataAccess.Models;

namespace RiaMoneyTransfer.DataAccess.Data
{
    public interface ICustomerData
    {
        Task<List<Customer>> ReadCustomerAsync();
        Task WriteCustomerAsync(string customer);
    }
}