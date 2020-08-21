using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BookChain.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlDbContext db = new SqlDbContext();
        public async Task Add(Employee employee)
        {
            try
            {
                var count = db.Employees.Where(s => s.Name.Equals(employee.Name) && s.Designation.Equals(employee.Designation)).Count();
                if (count == 0)
                {
                    db.Employees.Add(employee);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// get a specific  employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployee(int id)
        {
            try
            {
                Employee employee = await db.Employees.FindAsync(id);
                if (employee == null)
                {
                    return null;
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }
      /// <summary>
      /// Return all Employee
      /// </summary>
      /// <returns></returns>
        
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {

                var employees = await db.Employees.ToListAsync();
                return employees.AsQueryable();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> Update(Employee employee)
        {
            int val = -1;
            try
            {
                db.Entry(employee).State = EntityState.Modified;
                val = await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return val > 0;
        }
        /// <summary>
        /// Remove an Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            try
            {
                Employee employee = await db.Employees.FindAsync(id);
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// check if an employee exit based on name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool EmployeeExists(string name)
        {
            return db.Employees.Count(e => e.Name == name) > 0;
        }

    }
}