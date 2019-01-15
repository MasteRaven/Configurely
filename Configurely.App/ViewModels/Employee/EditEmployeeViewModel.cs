using System.Collections.Generic;

namespace Configurely.App.ViewModels.Employee
{
    public class EditEmployeeViewModel
    {
        public Entities.Employee employeeData { get; set; }

        public List<Entities.Department> Departments { get; set; }
    }
}