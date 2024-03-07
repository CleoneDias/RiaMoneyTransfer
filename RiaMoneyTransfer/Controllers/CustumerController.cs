using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RiaMoneyTransfer.DataAccess.Data;
using RiaMoneyTransfer.DataAccess.Models;
using RiaMoneyTransfer.DTOs;

namespace RiaMoneyTransfer.Controllers
{
    public class CustumerController : Controller
    {
        private readonly ICustomerData _data;

        public CustumerController(ICustomerData data)
        {
            _data = data;
        }

        [HttpGet("Customer")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                return Ok(await _data.GetCustomerAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Customer")]
        public async Task<IActionResult> InsertCustumer([FromBody] List<CustomerDto> customersDto)
        {
            try
            {
                var customers = new List<Customer>();
                foreach (var customerDto in customersDto)
                {
                    var customer = new Customer()
                    {
                        firstName = customerDto.firstName,
                        lastName = customerDto.lastName,
                        age = customerDto.age,
                        id = customerDto.id,
                    };
                    customers.Add(customer);
                }
                Customer.ValidateAsync(_data, customers);
                await _data.InsertCustomerAsync(customers);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
