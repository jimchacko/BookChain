using BookChain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
namespace BookChain.Controllers
{
    /// <summary>
    /// API for CRUD
    /// </summary>
    public class EmployeeApiController : ApiController
    {
        private readonly IEmployeeRepository _iEmployeeRepository = new EmployeeRepository();

        [HttpGet]
        [Route("api/Employees/Get")]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _iEmployeeRepository.GetEmployees();
        }

        [HttpPost]
        [Route("api/Employees/Create")]
        public async Task CreateAsync([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {

                await _iEmployeeRepository.Add(employee);
            }
        }

        [HttpGet]
        [Route("api/Employees/Details/{id}")]
        public async Task<Employee> Details(int id)
        {
            var result = await _iEmployeeRepository.GetEmployee(id);
            return result;
        }

        [HttpPut]
        [Route("api/Employees/Edit")]
        public async Task<bool> EditAsync([FromBody]Employee employee)
        {
            if (employee == null || employee.Name.Trim().Length == 0)
                return false;
            bool result = false;
            if (ModelState.IsValid)
            {
                result = await _iEmployeeRepository.Update(employee);

            }
            return result;
        }

        [HttpDelete]
        [Route("api/Employees/Delete/{id}")]
        public async Task DeleteConfirmedAsync(int id)
        {
            await _iEmployeeRepository.Delete(id);
        }
    }
}
