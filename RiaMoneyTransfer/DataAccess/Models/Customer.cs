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

        public static async Task<List<Customer>> ValidateAsync(ICustomerData data, List<Customer> customers)
        {
            var ids = new List<long>();
            var sortedCustomers = new List<Customer>();
            var savedCustomers = await data.ReadCustomerAsync();
            if (savedCustomers is not null && savedCustomers.Count > 0)
            {
                ids = savedCustomers.Select(x => x.id).ToList();
                sortedCustomers = savedCustomers.Concat(customers).ToList();
            }
            else
            {
                sortedCustomers = customers;
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
                    throw new Exception($"There is already a customer with this id: ({customer.id})");
                }
            }
            QuickSort(sortedCustomers, 0, sortedCustomers.Count - 1);
            return sortedCustomers;
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
            int lastNameComparison = String.Compare(p1.lastName, p2.lastName);
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
                return String.Compare(p1.firstName, p2.firstName) < 0;
            }
        }
    }
}
