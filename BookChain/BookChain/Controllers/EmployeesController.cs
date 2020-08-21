using BookChain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookChain.Controllers
{
    public class EmployeesController : Controller
    {
        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"]; // From Webconfig
        public async Task<ActionResult> Index()
        {
            IEnumerable<Employee> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync("employees/get");

                if (result.IsSuccessStatusCode)
                {
                    employees = await result.Content.ReadAsAsync<IList<Employee>>();
                }
                else
                {
                    employees = Enumerable.Empty<Employee>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(employees);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"employees/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employee = await result.Content.ReadAsAsync<Employee>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Address,Email,Company,Designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {

                if (employee == null || employee.Name.Trim().Length == 0 || employee.Email.Trim().Length == 0 || employee.Address.Trim().Length == 0)
                {
                    ModelState.AddModelError(string.Empty, "Name, Email and Address is mandatory");
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiBaseAddress);

                        var response = await client.PostAsJsonAsync("employees/Create", employee);


                        if (response.IsSuccessStatusCode)
                        {
                            await response.Content.ReadAsAsync<Employee>();

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }
                }
            }
            return View(employee);


        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"employees/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employee = await result.Content.ReadAsAsync<Employee>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,Email,Company,Designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee == null || employee.Name.Trim().Length == 0 || employee.Email.Trim().Length == 0 || employee.Address.Trim().Length == 0)
                {
                    ModelState.AddModelError(string.Empty, "Name, Email and Address is mandatory");
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiBaseAddress);
                        var response = await client.PutAsJsonAsync("employees/edit", employee);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"employees/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employee = await result.Content.ReadAsAsync<Employee>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var response = await client.DeleteAsync($"employees/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }
    }
}
