using Newtonsoft.Json;
using RiaMoneyTransfer.DataAccess.Data;

namespace RiaMoneyTransfer.DataAccess.Models
{
    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public long id { get; set; }

        private static object _lock = new object();

        public static void ValidateAsync(ICustomerData data, List<Customer> customers)
        {
            var ids = new List<long>();
            var savedCustomers = new List<Customer>();
            lock (_lock)
            {
                savedCustomers = data.GetCustomerAsync(false).Result;
            }
            if (savedCustomers is not null && savedCustomers.Count > 0)
            {
                ids = savedCustomers.Select(x => x.id).ToList();
            }
            foreach (var customer in customers)
            {
                if (string.IsNullOrWhiteSpace(customer.firstName))
                {
                    throw new Exception("Fist Name must be informed");
                }
                if (string.IsNullOrWhiteSpace(customer.lastName))
                {
                    throw new Exception("Last Name must be informed");
                }
                if (customer.age < 18)
                {
                    throw new Exception("Age must be above 18");
                }
                if (customer.id <= 0)
                {
                    throw new Exception("Id must be informed");
                }
                if (ids.Contains(customer.id))
                {
                    throw new Exception("There is already a customer with this id");
                }
            }
        }
    }
}
