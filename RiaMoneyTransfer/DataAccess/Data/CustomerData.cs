using Newtonsoft.Json;
using RiaMoneyTransfer.DataAccess.FileAccess;
using RiaMoneyTransfer.DataAccess.Models;

namespace RiaMoneyTransfer.DataAccess.Data
{
    public class CustomerData : ICustomerData
    {
        private readonly IReadWriteAccess _rw;

        public CustomerData(IReadWriteAccess rd)
        {
            _rw = rd;
        }

        public async Task<List<Customer>> GetCustomerAsync(bool sort = false)
        {
            var sortedCustomers = await _rw.ReadDataAsync<Customer>();
            if (sort)
            {
                QuickSort(sortedCustomers, 0, sortedCustomers.Count - 1);
            }
            return sortedCustomers;
        }

        public async Task InsertCustomerAsync(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                var data = JsonConvert.SerializeObject(customer);
                await _rw.WriteDataAsync(data);
            }
        }

        private static int Partition(List<Customer> list, int low, int high)
        {
            Customer pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (IsLessThan(list[j], pivot))
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, high);
            return i + 1;
        }

        private static void QuickSort(List<Customer> list, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);
                QuickSort(list, low, pi - 1);
                QuickSort(list, pi + 1, high);
            }
        }

        private static void Swap(List<Customer> list, int i, int j)
        {
            Customer temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        private static bool IsLessThan(Customer p1, Customer p2)
        {
            int lastNameComparison = string.Compare(p1.lastName, p2.lastName);
            if (lastNameComparison < 0)
            {
                return true;
            }
            else if (lastNameComparison > 0)
            {
                return false;
            }
            else
            {
                // If last names are equal, compare first names
                return string.Compare(p1.firstName, p2.firstName) < 0;
            }
        }
    }
}
