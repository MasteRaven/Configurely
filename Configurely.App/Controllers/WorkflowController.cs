using Configurely.App.ViewModels.Workflow;
using Configurely.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Configurely.App.Controllers
{
    public class WorkflowController : Controller
    {
        /// <summary>
        /// Action thats serves the view that lists all Departments
        /// </summary>
        public async Task<ActionResult> Index()
        {
            List<Department> oDepartment = null;

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost:51506/ConfigurelyService.svc/GetDepartments");

                var json = await client.GetStringAsync(uri);

                oDepartment = JsonConvert.DeserializeObject<List<Department>>(json);
            }

            return View(oDepartment);
        }

        /// <summary>
        /// Action thats serves the view that lists all Available and current Fields for a Department
        /// </summary>
        public async Task<ActionResult> Edit(int id)
        {
            Department oDepartment = null;
            List<Field> fieldList = null;
            WorkFlowViewModel vm = new WorkFlowViewModel();

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost:51506/ConfigurelyService.svc/GetDepartment/" + id);

                var json = await client.GetStringAsync(uri);

                oDepartment = JsonConvert.DeserializeObject<Department>(json);

                uri = new Uri("http://localhost:51506/ConfigurelyService.svc/GetFields");

                json = await client.GetStringAsync(uri);

                fieldList = JsonConvert.DeserializeObject<List<Field>>(json);

                vm.DepartmentId = oDepartment.ID;
                vm.DepartmentName = oDepartment.Name;
                vm.CurrentFields = oDepartment.Fields;

                vm.AvailableFields = fieldList.Where(p => vm.CurrentFields.All(p2 => p2.ID != p.ID)).ToList();
            }

            return View(vm);
        }

        /// <summary>
        /// Action thats Adds a new field to a Department
        /// </summary>
        public async Task<ActionResult> Add(int departmentId, int fieldId)
        {
            object oResult = null;

            Department oDepartment = new Department();
            oDepartment.ID = departmentId;
            oDepartment.Fields = new List<Field>() { new Field() { ID = fieldId } };

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri("http://localhost:51506/ConfigurelyService.svc/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(oDepartment);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                oResult = await client.PostAsync("http://localhost:51506/ConfigurelyService.svc/AddDepartmentField", stringContent);
            }

            return RedirectToAction("Edit", new { id = departmentId });
        }

        /// <summary>
        /// Action thats deletes a new field from a Department
        /// </summary>
        public async Task<ActionResult> Delete(int departmentId, int fieldId)
        {
            object oResult = null;

            Department oDepartment = new Department();
            oDepartment.ID = departmentId;
            oDepartment.Fields = new List<Field>() { new Field() { ID = fieldId } };


            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri("http://localhost:51506/ConfigurelyService.svc/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(oDepartment);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                oResult = await client.PostAsync("http://localhost:51506/ConfigurelyService.svc/DeleteDepartmentField", stringContent);
            }

            return RedirectToAction("Edit", new { id = departmentId });
        }
    }
}