using Configurely.App.ViewModels.Employee;
using Configurely.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Configurely.Entities.Response;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Text;

namespace Configurely.App.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Action thats return view with all the departments
        /// </summary>
        public async Task<ActionResult> Index()
        {
            List<Employee> employees = null;

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost:51506/ConfigurelyService.svc/GetEmployees");

                var json = await client.GetStringAsync(uri);

                employees = JsonConvert.DeserializeObject<List<Employee>>(json);
            }

            return View(employees);
        }

        /// <summary>
        /// Action thats return view that lets you read/edit/create a employee
        /// </summary>
        public ActionResult Edit(int? id, string type)
        {
            ViewBag.id = id;
            ViewBag.type = type;

            return View();
        }

        /// <summary>
        /// Action thats serves the React app with the viewModel for the employee
        /// </summary>
        public async Task<string> GetEmployeeData(int? id)
        {
            EditEmployeeViewModel vm = new EditEmployeeViewModel();

            using (var client = new HttpClient())
            {
                if(id != null)
                {
                    var uriEmployee = new Uri("http://localhost:51506/ConfigurelyService.svc/GetEmployee/" + id);

                    var jsonEmployee = await client.GetStringAsync(uriEmployee);

                    vm.employeeData = JsonConvert.DeserializeObject<Employee>(jsonEmployee);
                }
                else
                {
                    vm.employeeData = new Employee();
                }

                var uriDepartments = new Uri("http://localhost:51506/ConfigurelyService.svc/GetDepartments");

                var jsonDepartments = await client.GetStringAsync(uriDepartments);

                vm.Departments = JsonConvert.DeserializeObject<List<Department>>(jsonDepartments);
            }

            return JsonConvert.SerializeObject(vm);
        }

        /// <summary>
        /// Action thats lets react edit/create a employee
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> ChangeEmployee(Employee employeeData, string changeType)
        {
            HttpResponseMessage oResult = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri("http://localhost:51506/ConfigurelyService.svc/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(employeeData);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                if(changeType == "Edit")
                {
                    oResult = await client.PostAsync("http://localhost:51506/ConfigurelyService.svc/EditEmployee", stringContent);
                }
                else
                {
                    oResult = await client.PostAsync("http://localhost:51506/ConfigurelyService.svc/CreateEmployee", stringContent);
                }
            }

            return RedirectToAction("Index");
        }
    }
}