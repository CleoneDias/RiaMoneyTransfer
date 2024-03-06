using Newtonsoft.Json;
using RiaMoneyTransfer.DataAccess.FileAccess;
using RiaMoneyTransfer.DataAccess.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RiaMoneyTransfer.DataAccess.Data
{
    public class CustomerData : ICustomerData
    {
        private readonly IReadWriteAccess _rw;

        public CustomerData(IReadWriteAccess rd)
        {
            _rw = rd;
        }

        public async Task<List<Customer>> ReadCustomerAsync()
        {
            return await _rw.ReadDataAsync<Customer>();
        }

        public async Task WriteCustomerAsync(string customer)
        {
            await _rw.WriteDataAsync(customer);
        }
    }
}
